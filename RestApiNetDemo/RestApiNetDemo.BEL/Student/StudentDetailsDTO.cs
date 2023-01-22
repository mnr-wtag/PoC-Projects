using RestApiNetDemo.BEL.Enrollment;
using System.Collections.Generic;

namespace RestApiNetDemo.BEL.Student
{
    public class StudentDetailsDTO : StudentDTO
    {
        public virtual IEnumerable<EnrollmentDTO> Enrollments { get; set; }
    }
}
