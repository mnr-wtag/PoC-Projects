using RestApiNetDemo.BEL.Department;
using System.Collections.Generic;

namespace RestApiNetDemo.BEL.Employee.Teacher
{
    public class TeacherDetailsDTO : TeacherDTO
    {
        public IEnumerable<DepartmentDTO> Departments { get; set; }
    }
}