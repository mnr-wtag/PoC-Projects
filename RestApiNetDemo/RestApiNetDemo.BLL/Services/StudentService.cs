using RestApiNetDemo.BEL.Student;
using RestApiNetDemo.BLL.Helpers;
using RestApiNetDemo.BLL.IServices;
using RestApiNetDemo.DAL;
using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace RestApiNetDemo.BLL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student, int> _studentRepo;
        private readonly IRepository<AuthUser, int> _authRepo;
        private readonly IBulkInsert<Enrollment> _enrollmentRepo;

        public StudentService()
        {
            _studentRepo = DataAccessFactory.StudentDataAccess();
            _authRepo = DataAccessFactory.AuthUserDataAccess();
            _enrollmentRepo = DataAccessFactory.EnrollmentDataAccess();
        }

        public StudentService(IRepository<Student, int> studentRepo, IRepository<AuthUser, int> authRepo, IBulkInsert<Enrollment> enrollmentRepo)
        {
            _studentRepo = studentRepo;
            _authRepo = authRepo;
            _enrollmentRepo = enrollmentRepo;
        }

        public ServiceResponse GetStudentList()
        {
            try
            {
                var students = _studentRepo.GetAll();
                if (students == null)
                {
                    return new ServiceResponse(HttpStatusCode.NotFound, "No student was found");
                }
                List<StudentDTO> studentsListView = new List<StudentDTO>();

                foreach (Student student in students)
                {
                    StudentDTO studentView = new StudentDTO();

                    studentView.FirstName = student.FirstName;
                    studentView.MiddleName = student.MiddleName;
                    studentView.LastName = student.LastName;
                    studentView.StudentCardNumber = student.StudentCardNumber;
                    studentView.EnrollmentDate = student.EnrollmentDate;
                    studentView.DepartmentId = student.DepartmentId;
                    studentView.Id = student.Id;
                    studentView.DepartmentName = student.Department.Name;
                    studentsListView.Add(studentView);
                }

                return new ServiceResponse(HttpStatusCode.OK, "", studentsListView);
            }
            catch (Exception ex)
            {
                Log.Error($"{DateTime.UtcNow} - Class:{nameof(StudentService)} - Method:{nameof(GetStudentList)} --- Exception:{ex}");
                var _response = new ServiceResponse(HttpStatusCode.InternalServerError, "Something went wrong");
                return _response;
            }
        }

        public ServiceResponse GetStudentById(int id)
        {
            try
            {
                if (id <= 0) return new ServiceResponse(HttpStatusCode.BadRequest, "Invalid request");
                Student student = _studentRepo.GetById(x => x.Id == id);

                if (student == null) return new ServiceResponse(HttpStatusCode.NotFound, "Student not found");

                StudentDetailsDTO studentDetailsView = new StudentDetailsDTO
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    MiddleName = student.MiddleName,
                    LastName = student.LastName,
                    DepartmentId = student.DepartmentId,
                    DepartmentName = student.Department.Name,
                    StudentCardNumber = student.StudentCardNumber,
                    EnrollmentDate = student.EnrollmentDate,
                };

                return new ServiceResponse(HttpStatusCode.OK, "", studentDetailsView);
            }
            catch (Exception ex)
            {
                Log.Error($"{DateTime.UtcNow}  --- Class: {nameof(StudentService)}  --- Method: {nameof(GetStudentById)}  --- Exception:{ex}");
                return new ServiceResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }

        }

        public ServiceResponse GetStudentsByName(string name)
        {
            if (name == string.Empty) return new ServiceResponse(HttpStatusCode.BadRequest, "name cannot be empty");
            IEnumerable<Student> students = _studentRepo.GetAll(x => x.FirstName.Contains(name) || x.MiddleName.Contains(name) || x.LastName.Contains(name));
            if (students == null) return new ServiceResponse(HttpStatusCode.NotFound, "No student was found");

            List<StudentDTO> viewModelList = new List<StudentDTO>();
            foreach (Student student in students)
            {
                StudentDTO viewModel = new StudentDTO();
                viewModel.Id = student.Id;
                viewModel.FirstName = student.FirstName;
                viewModel.LastName = student.LastName;
                viewModelList.Add(viewModel);
            }

            return new ServiceResponse(HttpStatusCode.OK, "", viewModelList);
        }

        public ServiceResponse AddNewStudent(CreateStudentDTO viewModel)
        {
            try
            {

                if (viewModel == null)
                {
                    return new ServiceResponse(HttpStatusCode.NotFound, "Input not found");
                }

                IEnumerable<Student> existingStudents = _studentRepo.GetAll(x => x.StudentCardNumber == viewModel.StudentCardNumber);
                if (existingStudents != null)
                {
                    return new ServiceResponse(HttpStatusCode.Conflict, "Students with same ID already exists");
                }

                Student studentModel = new Student();
                studentModel.FirstName = viewModel.FirstName;
                studentModel.MiddleName = viewModel.MiddleName;
                studentModel.LastName = viewModel.LastName;
                studentModel.StudentCardNumber = viewModel.StudentCardNumber;
                studentModel.DepartmentId = viewModel.DepartmentId;
                studentModel.EnrollmentDate = viewModel.EnrollmentDate;
                studentModel.CreatedAt = DateTime.Now;
                studentModel.UpdatedAt = DateTime.Now;
                studentModel.CreatedBy = 1;
                studentModel.UpdatedBy = 1;

                var appliedCourses = viewModel.CourseList;

                foreach (var course in appliedCourses)
                {
                    if (Convert.ToInt32(course.DepartmentId) != viewModel.DepartmentId)
                    {

                        return new ServiceResponse(HttpStatusCode.BadRequest, "This course is not applicable with the selected department");
                    }
                }
                List<Enrollment> enrollmentList = new List<Enrollment>();
                foreach (var auth in appliedCourses)
                {
                    Enrollment enrollmentModel = new Enrollment();
                    enrollmentModel.CourseId = Convert.ToInt32(auth.Id);
                    enrollmentModel.StudentId = studentModel.Id;
                    enrollmentModel.CourseEnrollDate = DateTime.Now;
                    enrollmentModel.CreatedAt = DateTime.Now;
                    enrollmentModel.UpdatedAt = DateTime.Now;
                    enrollmentModel.CreatedBy = 1;
                    enrollmentModel.UpdatedBy = 1;

                    enrollmentList.Add(enrollmentModel);
                }
                IEnumerable<Enrollment> enrollments = enrollmentList;

                AuthUser authUser = new AuthUser();
                authUser.IsAdmin = false;
                authUser.UserName = viewModel.StudentCardNumber;
                authUser.Password = viewModel.StudentCardNumber;
                authUser.CreatedAt = DateTime.Now;
                authUser.UpdatedAt = DateTime.Now;
                authUser.CreatedBy = 1;
                authUser.UpdatedBy = 1;

                var studentResult = _studentRepo.Add(studentModel);
                var enrollentResult = _enrollmentRepo.BulkInsert(enrollments);
                var authResult = _authRepo.Add(authUser);
                if (studentResult && enrollentResult && authResult)
                {
                    return new ServiceResponse(HttpStatusCode.Created, "Student added successfully");
                }
                else
                {
                    return new ServiceResponse(HttpStatusCode.Conflict, "Student create failed");
                }
            }
            catch (Exception)
            {
                //Log the exception and rollback the transaction
                return new ServiceResponse(HttpStatusCode.InternalServerError, "Something went wrong.");
                throw;
            }
        }

        public ServiceResponse UpdateDepartment(StudentDTO viewModel)
        {
            if (viewModel == null) return new ServiceResponse(HttpStatusCode.BadRequest, "Input is null");
            Student model = _studentRepo.GetById(x => x.Id == viewModel.Id);
            if (model == null) return new ServiceResponse(HttpStatusCode.NotFound, "student not found");
            model.Id = viewModel.Id;
            model.FirstName = viewModel.FirstName;
            model.MiddleName = viewModel.MiddleName;
            model.LastName = viewModel.LastName;
            model.DepartmentId = viewModel.DepartmentId;
            model.StudentCardNumber = viewModel.StudentCardNumber;
            model.UpdatedAt = DateTime.Now;
            model.UpdatedBy = 1;

            var result = _studentRepo.Update(model);
            if (result)
            {
                return new ServiceResponse(HttpStatusCode.NoContent, "student updated successfully");
            }
            else
            {
                return new ServiceResponse(HttpStatusCode.BadRequest, "student update failed");
            }
        }

        public ServiceResponse DeleteStudent(int id)
        {
            Student model = _studentRepo.GetById(x => x.Id == id);
            if (model == null) return new ServiceResponse(HttpStatusCode.NotFound, "student not found");
            var result = _studentRepo.Delete(id);
            if (result)
            {
                return new ServiceResponse(HttpStatusCode.NoContent,"student deleted successfully");
            }
            else
            {
                return new ServiceResponse(HttpStatusCode.BadRequest, "student delete failed");
            }

        }


    }
}