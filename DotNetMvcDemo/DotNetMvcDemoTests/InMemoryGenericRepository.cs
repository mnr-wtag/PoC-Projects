using DotNetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetMvcDemoTests
{
    public class InMemoryGenericRepository
    {
        private readonly List<Department> _db = new List<Department>();

        public Exception ExceptionToThrow { get; set; }

        public IEnumerable<Department> GetAllEmployee()
        {
            return _db.ToList();
        }

        public Department GetEmployeeByID(int id)
        {
            return _db.FirstOrDefault(d => d.Id == id);
        }

        public void CreateNewEmployee(Department employeeToCreate)
        {
            if (ExceptionToThrow != null)
                throw ExceptionToThrow;

            _db.Add(employeeToCreate);
        }

        public void SaveChanges(Department deptToUpdate)
        {

            foreach (Department dept in _db)
            {
                if (dept.Id != deptToUpdate.Id) continue;
                _db.Remove(dept);
                _db.Add(deptToUpdate);
                break;
            }
        }

        public void Add(Department deptToAdd)
        {
            _db.Add(deptToAdd);
        }


        public int SaveChanges()
        {
            return 1;
        }

        public void DeleteEmployee(int id)
        {
            _db.Remove(GetEmployeeByID(id));
        }
    }
}
