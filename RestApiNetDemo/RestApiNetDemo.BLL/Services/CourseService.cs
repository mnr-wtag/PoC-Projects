using RestApiNetDemo.BEL.Course;
using RestApiNetDemo.DAL;
using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using System;
using System.Collections.Generic;

namespace RestApiNetDemo.BLL.Services
{
    public class CourseService
    {
        private readonly IRepository<Cours, int> _repository;

        public CourseService(IRepository<Cours, int> repo)
        {
            _repository = repo;
        }

        public CourseService()
        {
            _repository = DataAccessFactory.CourseDataAccess();
        }

        public  IEnumerable<CourseDTO> GetCourseList()
        {

            var courses = _repository.GetAll();
            List<CourseDTO> coursesViewList = new List<CourseDTO>();

            foreach (var course in courses)
            {
                var courseView = new CourseDTO();
                courseView.Id = course.Id;
                courseView.Name = course.Name;
                courseView.Credit = course.Credit;
                courseView.DepartmentName = course.Department.Name;
                courseView.DepartmentId = course.Department.Id;
                coursesViewList.Add(courseView);
            }
            return coursesViewList;
        }

        public CourseDetailsDTO GetCourseById(int id)
        {
            var course = _repository.GetById(x => x.Id == id);
            if (course == null) return null;

            var courseView = new CourseDetailsDTO();
            courseView.Id = course.Id;
            courseView.Name = course.Name;
            courseView.Enrollments = course.Enrollments as IEnumerable<BEL.Enrollment.EnrollmentDTO>;
            courseView.Credit = course.Credit;
            courseView.DepartmentName = course.Department.Name;

            return courseView;
        }



        public bool AddNewCourse(CreateCourseDTO viewModel)
        {
            try
            {
                var course = new Cours();
                course.Name = viewModel.Name;
                course.Credit = viewModel.Credit;
                course.CreatedAt = DateTime.Now;
                course.UpdatedAt = DateTime.Now;
                course.DepartmentId = viewModel.DepartmentId;
                course.CreatedBy = 1;
                course.UpdatedBy = 1;

                

                _repository.Add(course);
               

               
                return true;
            }
            catch (Exception)
            {
                //Log the exception and rollback the transaction
               
                throw;
            }
        }

        public bool UpdateCourse(CourseDTO viewModel)
        {
            if (viewModel == null) return false;
            var model = _repository.GetById(x=>x.Id==viewModel.Id);
            if (model == null) return false;
            model.Name = viewModel.Name;
            model.Credit = viewModel.Credit;
            model.DepartmentId = viewModel.DepartmentId;
            model.UpdatedAt = DateTime.Now;
            model.UpdatedBy = 1;

            _repository.Update(model);
            return true;
        }


        public bool DeleteCourse(int id)
        {
            var course = _repository.GetById( x => x.Id == id);

            if (course == null) return false;
            _repository.Delete(id);
            return true;
        }
    }
}
