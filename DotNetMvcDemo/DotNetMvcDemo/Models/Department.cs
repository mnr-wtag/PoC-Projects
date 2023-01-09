using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetMvcDemo.Models
{
    public class Department : DateAndAuthor
    {
        public Department()
        {
            Students = new HashSet<Student>();
            Courses = new HashSet<Course>();
            Teachers = new HashSet<Teacher>();
            Admins = new HashSet<Admin>();

        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<Admin> Admins { get; set; }
        public virtual AuthUser UpdaterUser { get; set; }
        public virtual AuthUser CreatorUser { get; set; }
    }
}