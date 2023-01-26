using RestApiNetDemo.BEL.Course;
using RestApiNetDemo.BLL.Helpers;

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