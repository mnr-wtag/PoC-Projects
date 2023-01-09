using System.ComponentModel.DataAnnotations;

namespace DotNetMvcDemo.ViewModels.Course
{
    public class CourseViewModel : CreateCourseViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
    }
}