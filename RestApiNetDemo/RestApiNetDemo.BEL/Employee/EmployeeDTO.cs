using RestApiNetDemo.BEL.AuditableEntities;
using System.ComponentModel.DataAnnotations;

namespace RestApiNetDemo.BEL.Employee
{
    public class EmployeeDTO : DateAndAuthorDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [DataType(DataType.Currency)]
        public int Salary { get; set; }

        public string FullName => FirstName + " " + LastName;
    }
}