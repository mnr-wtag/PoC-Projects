using RestApiNetDemo.BEL.Department;
using System.Collections.Generic;

namespace RestApiNetDemo.BEL.Employee.Admin
{
    public class AdminDetailsDTO : AdminDTO
    {
        public IEnumerable<DepartmentDTO> Departments { get; set; }
    }
}