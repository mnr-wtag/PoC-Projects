using DotNetMvcDemo.Data;
using DotNetMvcDemo.Models;
using DotNetMvcDemo.Repository;
using DotNetMvcDemo.UnitOfWork;
using DotNetMvcDemo.ViewModels.Course;
using System;
using System.Collections.Generic;


namespace DotNetMvcDemo.Services
{
    public class CourseService
    {
        private readonly UnitOfWork<ApplicationDbContext> _unitOfWork = new UnitOfWork<ApplicationDbContext>();
        private readonly IGenericRepository<Course> _repository;

        public CourseService()
        {
            //If you want to use Generic Repository with Unit of work
            _repository = new GenericRepository<Course>(_unitOfWork);

        }

        public CourseService(IGenericRepository<Course> repository)
        {
            _repository = repository;
        }

        public IEnumerable<CourseViewModel> GetCourseList()
        {

            IEnumerable<Course> courses = _repository.GetAll();
            List<CourseViewModel> coursesViewList = new List<CourseViewModel>();

            foreach (Course course in courses)
            {
                CourseViewModel courseView = new CourseViewModel();
                courseView.Id = course.Id;
                courseView.Name = course.Name;
                courseView.Credit = course.Credit;
                courseView.DepartmentName = course.Department.Name;
                coursesViewList.Add(courseView);
            }

            return coursesViewList;
        }

        public CourseDetailsViewModel GetCourseById(int? id)
        {
            Course course = _repository.GetById(x => x.Id == id, new List<string> { "Enrollments" });
            if (course == null) return null;

            CourseDetailsViewModel courseView = new CourseDetailsViewModel();
            courseView.Id = course.Id;
            courseView.Name = course.Name;
            courseView.Enrollments = course.Enrollments;
            courseView.Credit = course.Credit;
            courseView.DepartmentName = course.Department.Name;
            courseView.Enrollments = course.Enrollments;
            return courseView;
        }



        public bool AddNewCourse(CreateCourseViewModel viewModel)
        {
            try
            {
                Course course = new Course();
                course.Name = viewModel.Name;
                course.Credit = viewModel.Credit;
                course.CreatedAt = DateTime.Now;
                course.UpdatedAt = DateTime.Now;
                course.DepartmentId = viewModel.DepartmentId;
                course.CreatedBy = 1;
                course.UpdatedBy = 1;

                _unitOfWork.CreateTransaction();

                _repository.Insert(course);
                _unitOfWork.Save();

                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                //Log the exception and rollback the transaction
                _unitOfWork.Rollback();
                throw;
            }
        }

        public bool UpdateCourse(CourseViewModel viewModel)
        {
            if (viewModel == null) return false;
            Course model = _repository.GetById(x => x.Id == viewModel.Id);
            if (model == null) return false;
            model.Name = viewModel.Name;
            model.Credit = viewModel.Credit;
            model.DepartmentId = viewModel.DepartmentId;
            model.UpdatedAt = DateTime.Now;
            model.UpdatedBy = 1;

            _repository.Update(model);
            _unitOfWork.Save();
            return true;
        }


        public bool DeleteCourse(int? id)
        {
            Course course = _repository.GetById(x => x.Id == id);

            if (course == null) return false;
            _repository.Delete(course);
            _unitOfWork.Save();
            return true;
        }

    }
}