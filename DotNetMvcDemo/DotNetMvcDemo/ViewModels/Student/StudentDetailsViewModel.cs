using DotNetMvcDemo.Models;
using System.Collections.Generic;

namespace DotNetMvcDemo.ViewModels.Student
{
    public class StudentDetailsViewModel : StudentViewModel
    {
        public virtual IEnumerable<Enrollment> Enrollments { get; set; }
    }
}