using DotNetMvcDemo.Models;
using System.Collections.Generic;

namespace DotNetMvcDemo.ViewModels.Course
{
    public class CourseDetailsViewModel : CourseViewModel
    {

        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}