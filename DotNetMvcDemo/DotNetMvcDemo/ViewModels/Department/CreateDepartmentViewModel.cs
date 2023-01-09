using System.ComponentModel.DataAnnotations;

namespace DotNetMvcDemo.ViewModels.Department
{
    public class CreateDepartmentViewModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Department Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Department Description")]
        public string Description { get; set; }
    }
}