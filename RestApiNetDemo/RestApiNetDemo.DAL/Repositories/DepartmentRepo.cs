using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RestApiNetDemo.DAL.Repositories
{
    internal class DepartmentRepo : IRepository<Department, int, string>
    {
        private readonly DotNetMvcDbEntities _dbEntities;


        public DepartmentRepo(DotNetMvcDbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }



        public bool Add(Department entity)
        {
            _dbEntities.Departments.Add(entity);
            int result = _dbEntities.SaveChanges();
            return result != 0;
        }

        public bool Delete(int id)
        {
            Department department = _dbEntities.Departments.FirstOrDefault(s => s.Id == id);
            if (department == null) return false;
            _dbEntities.Departments.Remove(department);
            int result = _dbEntities.SaveChanges();
            return result != 0;
        }

        public List<Department> GetAll()
        {
            List<Department> departmentList = _dbEntities.Departments.ToList();
            return departmentList;
        }

        public Department GetById(int id)
        {
            Department department = _dbEntities.Departments.FirstOrDefault(s => s.Id == id);
            return department;
        }

        public List<Department> Search(string search)
        {
            var department = _dbEntities.Departments.Where(s => s.Name.Contains(search)).ToList();
            return department;
        }

        public bool Update(Department entity)
        {
            _dbEntities.Entry(entity).State = EntityState.Modified;
            int result = _dbEntities.SaveChanges();
            return result != 0;
        }
    }
}
