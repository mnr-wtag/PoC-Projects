using RestApiNetDemo.BEL.AuditableEntities;
using System.ComponentModel.DataAnnotations;

namespace RestApiNetDemo.BEL.Course
{
    public class CreateCourseDTO : DateAndAuthorDTO
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