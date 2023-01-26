using System.ComponentModel.DataAnnotations;

namespace RestApiNetDemo.BEL.Course
{
    public class CourseDTO : CreateCourseDTO
    {
        public int Id { get; set; }

        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
    }
}