using RestApiNetDemo.BEL.Enrollment;
using System.Collections.Generic;

namespace RestApiNetDemo.BEL.Course
{
    public class CourseDetailsDTO:CourseDTO
    {
        public IEnumerable<EnrollmentDTO> Enrollments { get; set; }
       
    }
}
