using RestApiNetDemo.BEL.Employee.Admin;
using RestApiNetDemo.BEL.Employee.Teacher;
using System.Collections.Generic;

namespace RestApiNetDemo.BEL.Department
{
    public class DepartmentDetailsDTO : DepartmentDTO
    {
        public ICollection<TeacherDTO> DepartmentTeachers { get; set; }
        public ICollection<AdminDTO> DepartmentAdmins { get; set; }
    }
}
