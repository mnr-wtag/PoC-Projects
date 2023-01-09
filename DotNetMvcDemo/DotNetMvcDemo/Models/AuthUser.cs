using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotNetMvcDemo.Models
{
    public class AuthUser : DateAndAuthor
    {
        public AuthUser()
        {
            AdminsUpdated = new HashSet<Admin>();
            AdminsCreated = new HashSet<Admin>();
            CoursesUpdated = new HashSet<Course>();
            CoursesCreated = new HashSet<Course>();
            DepartmentsCreated = new HashSet<Department>();
            DepartmentsUpdated = new HashSet<Department>();
            StudentsUpdated = new HashSet<Student>();
            StudentsCreated = new HashSet<Student>();
            EnrollmentsUpdated = new HashSet<Enrollment>();
            EnrollmentsCreated = new HashSet<Enrollment>();
            TeachersUpdated = new HashSet<Teacher>();
            TeachersCreated = new HashSet<Teacher>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public virtual UserProfile UserProfile { get; set; }


        //public virtual Student Student { get; set; }


        //public virtual Teacher Teacher { get; set; }


        //public virtual Admin Admin { get; set; }

        public virtual ICollection<Department> DepartmentsUpdated { get; set; }
        public virtual ICollection<Department> DepartmentsCreated { get; set; }

        public virtual ICollection<Course> CoursesUpdated { get; set; }
        public virtual ICollection<Course> CoursesCreated { get; set; }

        public virtual ICollection<Student> StudentsUpdated { get; set; }
        public virtual ICollection<Student> StudentsCreated { get; set; }

        public virtual ICollection<Enrollment> EnrollmentsUpdated { get; set; }
        public virtual ICollection<Enrollment> EnrollmentsCreated { get; set; }

        public virtual ICollection<Teacher> TeachersUpdated { get; set; }
        public virtual ICollection<Teacher> TeachersCreated { get; set; }

        public virtual ICollection<Admin> AdminsUpdated { get; set; }
        public virtual ICollection<Admin> AdminsCreated { get; set; }
    }
}