using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetMvcDemo.Models
{
    public class Employee : DateAndAuthor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [DataType(DataType.Currency)]
        public int Salary { get; set; }
    }

    public class Admin : Employee
    {
        public Admin()
        {
            Departments = new HashSet<Department>();
        }

        public string AdminCardNumber { get; set; }

        //[ForeignKey("UserId")]
        //public int UserId { get; set; }

        public AuthUser User { get; set; }

        public AuthUser UpdaterUser { get; set; }

        public AuthUser CreatorUser { get; set; }

        public ICollection<Department> Departments { get; set; }

    }

    public class Teacher : Employee
    {
        public Teacher()
        {
            Departments = new HashSet<Department>();
        }

        public string TeacherCardNumber { get; set; }
        public bool IsInProbation { get; set; } = false;

        //[ForeignKey("UserId")]
        //public int UserId { get; set; }

        public AuthUser User { get; set; }
        public AuthUser UpdaterUser { get; set; }

        public AuthUser CreatorUser { get; set; }



        public ICollection<Department> Departments { get; set; }
    }
}