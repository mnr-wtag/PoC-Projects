using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RestApiNetDemo.BEL.Entities.Course
{
    public class CreateCourseDTO
    {
        [Required]
        [Display(Name = "Course Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Course Credit")]
        public int Credit { get; set; }

        public int DepartmentId { get; set; }
    }
}
