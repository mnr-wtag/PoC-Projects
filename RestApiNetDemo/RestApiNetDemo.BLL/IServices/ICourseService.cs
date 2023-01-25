using RestApiNetDemo.BEL.Course;
using RestApiNetDemo.BLL.Helpers;
using System.Collections.Generic;

namespace RestApiNetDemo.BLL.IServices
{
    public interface ICourseService
    {
        ServiceResponse GetCourseList();
        ServiceResponse GetCourseById(int id);
        ServiceResponse AddNewCourse(CreateCourseDTO viewModel);
        ServiceResponse UpdateCourse(CourseDTO viewModel);
        ServiceResponse DeleteCourse(int id);
    }
}
