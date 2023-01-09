using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetMvcDemo.Models
{
    public class Student : DateAndAuthor
    {
        public Student()
        {
            Enrollments = new HashSet<Enrollment>();

        }

        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Middle name cannot be longer than 50 characters.")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }


        public string StudentCardNumber { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; } = DateTime.Today;

        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }


        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AuthUser User { get; set; }

        public virtual AuthUser UpdaterUser { get; set; }

        public virtual AuthUser CreatorUser { get; set; }


    }
}