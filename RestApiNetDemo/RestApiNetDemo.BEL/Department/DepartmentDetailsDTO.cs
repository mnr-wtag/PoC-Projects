using RestApiNetDemo.BEL.Entities.Admin;
using RestApiNetDemo.BEL.Entities.Department;
using RestApiNetDemo.BEL.Entities.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestApiNetDemo.BEL.Department
{
    public class DepartmentDetailsDTO:DepartmentDTO
    {
        public IEnumerable<Teacher> DepartmentTeachers { get; set; }
        public IEnumerable<Admin> DepartmentAdmins { get; set; }
    }
}
