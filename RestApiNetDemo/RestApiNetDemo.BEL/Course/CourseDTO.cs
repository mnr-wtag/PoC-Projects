using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RestApiNetDemo.BEL.Entities.Course
{
    public class CourseDTO: CreateCourseDTO
    {
        public int Id { get; set; }

        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
    }
}
