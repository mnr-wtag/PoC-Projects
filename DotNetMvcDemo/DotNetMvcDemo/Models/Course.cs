using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetMvcDemo.Models
{
    public class Course : DateAndAuthor
    {
        public Course()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Course name cannot be longer than 50 characters.")]
        public string Name { get; set; }
        [Required]
        public int Credit { get; set; }

        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }


        public virtual AuthUser UpdaterUser { get; set; }

        public virtual AuthUser CreatorUser { get; set; }
    }
}