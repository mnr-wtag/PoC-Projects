using RestApiNetDemo.BEL.Course;
using RestApiNetDemo.BLL.Helpers;
using RestApiNetDemo.BLL.IServices;
using RestApiNetDemo.DAL;
using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;

namespace RestApiNetDemo.BLL.Services
{
    public class CourseService : ICourseService
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

        public ServiceResponse GetCourseList()
        {
            try
            {
                var courses = _repository.GetAll();
                if (courses == null) return new ServiceResponse(HttpStatusCode.NotFound, "No course was found");
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

                var response = new ServiceResponse(HttpStatusCode.OK, "", coursesViewList);
                return response;
            }
            catch (Exception ex)
            {
                Log.Error($"{DateTime.UtcNow} - Class:{nameof(CourseService)} - Method:{nameof(GetCourseList)} --- Exception:{ex}");
                var _response = new ServiceResponse(HttpStatusCode.InternalServerError, "Something went wrong");
                return _response;
            }
        }

        public ServiceResponse GetCourseById(int id)
        {
            try
            {
                var course = _repository.GetById(x => x.Id == id, new List<string> { "Enrollments" });

                if (course == null)
                {
                    return new ServiceResponse(HttpStatusCode.NotFound, "Course not found");
                }
                var courseView = new CourseDetailsDTO();
                courseView.Id = course.Id;
                courseView.Name = course.Name;
                courseView.Enrollments = course.Enrollments as IEnumerable<BEL.Enrollment.EnrollmentDTO>;
                courseView.Credit = course.Credit;
                courseView.DepartmentName = course.Department.Name;

                return new ServiceResponse(HttpStatusCode.OK, "Course found", courseView);
            }
            catch (Exception ex)
            {
                Log.Error($"{DateTime.UtcNow}  --- Class: {nameof(CourseService)}  --- Method: {nameof(GetCourseById)}  --- Exception:{ex}");
                return new ServiceResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }

        public ServiceResponse AddNewCourse(CreateCourseDTO viewModel)
        {
            try
            {
                if (viewModel == null) return new ServiceResponse(HttpStatusCode.BadRequest, "Course cannot be null");
                var course = new Cours();
                course.Name = viewModel.Name;
                course.Credit = viewModel.Credit;
                course.CreatedAt = DateTime.Now;
                course.UpdatedAt = DateTime.Now;
                course.DepartmentId = viewModel.DepartmentId;
                course.CreatedBy = 1;
                course.UpdatedBy = 1;

                var result = _repository.Add(course);
                return result
                    ? new ServiceResponse(HttpStatusCode.Created, $"Course created successfully")
                    : new ServiceResponse(HttpStatusCode.BadRequest, "Course create failed");
            }
            catch (Exception ex)
            {
                //Log the exception and rollback the transaction
                Log.Error($"{DateTime.UtcNow} --- Class:{nameof(CourseService)} --- Method:{nameof(AddNewCourse)} --- Exception:{ex}");
                var _response = new ServiceResponse(HttpStatusCode.InternalServerError, "Something went wrong");
                return _response;
            }
        }

        public ServiceResponse UpdateCourse(CourseDTO viewModel)
        {
            try
            {
                if (viewModel == null) return new ServiceResponse(HttpStatusCode.BadRequest, "Course Update cannot be null");
                var model = _repository.GetById(x => x.Id == viewModel.Id);
                if (model == null) return new ServiceResponse(HttpStatusCode.NotFound, "Course not found");
                model.Name = viewModel.Name;
                model.Credit = viewModel.Credit;
                model.DepartmentId = viewModel.DepartmentId;
                model.UpdatedAt = DateTime.Now;
                model.UpdatedBy = 1;

                var result = _repository.Update(model);
                return result
                    ? new ServiceResponse(HttpStatusCode.NoContent, "Course updated successfully")
                    : new ServiceResponse(HttpStatusCode.BadRequest, "Course update failed");
            }
            catch (Exception ex)
            {
                Log.Error($"{DateTime.UtcNow} --- Class:{nameof(CourseService)} --- Method:{nameof(UpdateCourse)} --- Exception:{ex}");
                var _response = new ServiceResponse(HttpStatusCode.InternalServerError, "Something went wrong");
                return _response;
            }
        }

        public ServiceResponse DeleteCourse(int id)
        {
            try
            {
                var course = _repository.GetById(x => x.Id == id);
                if (course == null) return new ServiceResponse(HttpStatusCode.NotFound, "Course not found");
                var result = _repository.Delete(id);
                return result
                    ? new ServiceResponse(HttpStatusCode.NoContent, "Course deleted successfully")
                    : new ServiceResponse(HttpStatusCode.BadRequest, "Course delete failed");
            }
            catch (Exception ex)
            {
                Log.Error($"{DateTime.UtcNow} --- Class:{nameof(CourseService)} --- Method:{nameof(DeleteCourse)} --- Exception:{ex}");
                return new ServiceResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }
    }
}