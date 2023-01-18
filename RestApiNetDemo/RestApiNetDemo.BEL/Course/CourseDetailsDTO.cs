using RestApiNetDemo.BEL.Entities.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiNetDemo.BEL.Course
{
    internal class CourseDetailsDTO
    {
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}
