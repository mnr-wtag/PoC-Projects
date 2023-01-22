using RestApiNetDemo.DAL.Data;
using System.Collections.Generic;
using System;
using RestApiNetDemo.BEL.Student;
using RestApiNetDemo.DAL.IRepositories;
using RestApiNetDemo.DAL;
using RestApiNetDemo.BLL.Helpers;
using System.Web;

namespace RestApiNetDemo.BLL.Services
{
    public class StudentService
    {
        private readonly IRepository<Student, int> _studentRepo;
        private readonly IRepository<AuthUser, int> _authRepo;
        private readonly IRepository<Enrollment,int> _enrollmentRepo;
        public StudentService()
        {
            _studentRepo = DataAccessFactory.StudentDataAccess();
            _authRepo= DataAccessFactory.AuthUserDataAccess();
            _enrollmentRepo = DataAccessFactory.EnrollmentDataAccess();
        }

        public StudentService(IRepository<Student, int> studentRepo, IRepository<AuthUser, int> authRepo, IRepository<Enrollment, int> enrollmentRepo)
        {
            _studentRepo = studentRepo;
            _authRepo = authRepo;
            _enrollmentRepo = enrollmentRepo;

        }

        public IEnumerable<StudentDTO> GetStudentList()
        {
            try
            {
                var students = _studentRepo.GetAll();
                List<StudentDTO> studentsViewList = new List<StudentDTO>();

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
                    studentsViewList.Add(studentView);
                }

                return studentsViewList;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public StudentDetailsDTO GetStudentById(int id)
        {
            Student student = _studentRepo.GetById(x => x.Id == id);

            if (student == null) return null;

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

            return studentDetailsView;
        }

        public List<StudentDTO> GetStudentsByName(string name)
        {

            IEnumerable<Student> students = _studentRepo.GetAll(x => x.FirstName.Contains(name) || x.LastName.Contains(name));

            if (students == null) return null;

            List<StudentDTO> viewModelList = new List<StudentDTO>();
            foreach (Student student in students)
            {
                StudentDTO viewModel = new StudentDTO();
                viewModel.Id = student.Id;
                viewModel.FirstName = student.FirstName;
                viewModel.LastName = student.LastName;
                viewModelList.Add(viewModel);
            }

            return viewModelList;
        }

        public ServiceResponse AddNewStudent(CreateStudentDTO viewModel)
        {
            try
            {
                ServiceResponse response = new ServiceResponse();
                if (viewModel == null)
                {
                    response.Response = Response.NotFound;
                    response.Message = "Input not found";
                    return response;
                }

                IEnumerable<Student> existingStudents = _studentRepo.GetAll(x => x.StudentCardNumber == viewModel.StudentCardNumber);
                if (existingStudents != null)
                {
                    response.Response = Response.Exists;
                    response.Message = "Students with same ID already exists";
                    return response;
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

                foreach (var auth in appliedCourses)
                {
                    if (Convert.ToInt32(auth.DepartmentId) != viewModel.DepartmentId)
                    {
                        response.Response = Response.Error;
                        response.Message = "This auth is not applicable with the selected department";
                        return response;
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



                _studentRepo.Add(studentModel);
                _enrollmentRepo.BulkInsert(enrollments);
                _authRepo.Add(authUser);


                response.Response = Response.Success;
                response.Message = "Student added successfully";
                return response;
            }
            catch (Exception)
            {
                //Log the exception and rollback the transaction

                throw;
            }
        }

        public bool UpdateDepartment(StudentDTO viewModel)
        {
            if (viewModel == null) return false;
            Student model = _studentRepo.GetById(x => x.Id == viewModel.Id);
            if (model == null) return false;
            model.Id = viewModel.Id;
            model.FirstName = viewModel.FirstName;
            model.MiddleName = viewModel.MiddleName;
            model.LastName = viewModel.LastName;
            model.DepartmentId = viewModel.DepartmentId;
            model.StudentCardNumber = viewModel.StudentCardNumber;
            model.UpdatedAt = DateTime.Now;
            model.UpdatedBy = 1;

            var result = _studentRepo.Update(model);
            return result;
        }


        public bool DeleteStudent(int id)
        {
            Student model = _studentRepo.GetById(x => x.Id == id);
            if (model == null) return false;
            var result = _studentRepo.Delete(id);
            return result;

        }
    }
}
