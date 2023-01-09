using RestApiNetDemo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestApiNetDemo.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public IEnumerable<Employee> GetEmployeesByDepartment(string Dept)
        {
            return _context.Employees.Where(emp=>emp.Dept == Dept).ToList();
        }

        public IEnumerable<Employee> GetEmployeesByGender(string Gender)
        {
            return _context.Employees.Where(emp => emp.Gender == Gender).ToList();
        }
    }
}