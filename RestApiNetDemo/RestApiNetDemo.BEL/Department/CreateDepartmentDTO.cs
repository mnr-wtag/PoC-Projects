using System.ComponentModel.DataAnnotations;

namespace RestApiNetDemo.BEL.Department
{
    public class CreateDepartmentDTO
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
