using RestApiNetDemo.BEL.Teacher;
using RestApiNetDemo.BEL.Admin;
using System.Collections.Generic;

namespace RestApiNetDemo.BEL.Department
{
    public class DepartmentDetailsDTO : DepartmentDTO
    {
        public ICollection<TeacherDTO> DepartmentTeachers { get; set; }
        public ICollection<Admin.Admin> DepartmentAdmins { get; set; }
    }
}
