using System.Collections.Generic;

namespace RestApiNetDemo.BEL.Department
{
    public class DepartmentDetailsDTO : DepartmentDTO
    {
        public ICollection<TeacherDTO> DepartmentTeachers { get; set; }
        public ICollection<Admin.AdminDTO> DepartmentAdmins { get; set; }
    }
}
