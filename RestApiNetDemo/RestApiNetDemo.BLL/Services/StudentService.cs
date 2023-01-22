using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.UnitOfWork;
using System.Collections.Generic;
using System;
using RestApiNetDemo.BEL.Student;
using RestApiNetDemo.DAL.IRepositories;
using RestApiNetDemo.DAL;

namespace RestApiNetDemo.BLL.Services
{
    public class StudentService
    {
        private readonly IRepository<Student, int> _repo;
        public StudentService()
        {
            _repo = DataAccessFactory.StudentDataAccess();
          
        }

        public StudentService(IRepository<Student, int> repo)
        {
            _repo = repo;
        }

        public IEnumerable<StudentDTO> GetStudentList()
        {

            IEnumerable<Student> students = _repo.GetAll();
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

        public StudentDetailsDTO GetStudentById(int? id)
        {
            Student student = _repo.GetById(x => x.Id == id);

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

            IEnumerable<Student> students = _repo.GetAll(x => x.FirstName.Contains(name) || x.LastName.Contains(name));

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

                IEnumerable<Student> existingStudents = _repo.GetAll(x => x.StudentCardNumber == viewModel.StudentCardNumber);
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

                List<System.Web.Mvc.SelectListItem> appliedCourses = viewModel.CourseList.ToList();

                foreach (System.Web.Mvc.SelectListItem course in appliedCourses)
                {
                    if (Convert.ToInt32(course.Value) != viewModel.DepartmentId)
                    {
                        response.Response = Response.Error;
                        response.Message = "This course is not applicable with the selected department";
                        return response;
                    }
                }
                List<Enrollment> enrollmentList = new List<Enrollment>();
                foreach (System.Web.Mvc.SelectListItem course in appliedCourses)
                {
                    Enrollment enrollmentModel = new Enrollment();
                    enrollmentModel.CourseId = Convert.ToInt32(course.Value);
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


                _unitOfWork.CreateTransaction();

                _repo.Insert(studentModel);
                _enrollmentRepository.BulkInsert(enrollments);
                _authRepository.Insert(authUser);

                _unitOfWork.Save();
                _unitOfWork.Commit();

                response.Response = Response.Success;
                response.Message = "Student added successfully";
                return response;
            }
            catch (Exception)
            {
                //Log the exception and rollback the transaction
                _unitOfWork.Rollback();
                throw;
            }
        }

        public bool UpdateDepartment(StudentDTO viewModel)
        {
            if (viewModel == null) return false;
            Student model = _repo.GetById(x => x.Id == viewModel.Id);
            if (model == null) return false;
            model.Id = viewModel.Id;
            model.FirstName = viewModel.FirstName;
            model.MiddleName = viewModel.MiddleName;
            model.LastName = viewModel.LastName;
            model.DepartmentId = viewModel.DepartmentId;
            model.StudentCardNumber = viewModel.StudentCardNumber;
            model.UpdatedAt = DateTime.Now;
            model.UpdatedBy = 1;

            _repo.Update(model);
            _unitOfWork.Save();
            return true;
        }


        public bool DeleteStudent(int? id)
        {
            Student model = _repo.GetById(x => x.Id == id);
            if (model == null) return false;
            _repo.Delete(model);
            _unitOfWork.Save();
            return true;

        }
    }
}
