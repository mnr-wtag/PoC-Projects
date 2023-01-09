using System.ComponentModel.DataAnnotations;

namespace DotNetMvcDemo.ViewModels.Course
{
    public class CreateCourseViewModel
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