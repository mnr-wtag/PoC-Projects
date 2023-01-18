using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RestApiNetDemo.DAL.Repositories
{
    internal class DepartmentRepo : IRepository<Department, int>
    {
        private readonly DotNetMvcDbEntities _dbEntities;
        private readonly IRepository<Department, int> _repo;

        public DepartmentRepo()
        {
            _dbEntities = new DotNetMvcDbEntities();
        }

        public DepartmentRepo(IRepository<Department, int> repo)
        {
            _repo = repo;
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

        public bool Update(Department entity)
        {
            _dbEntities.Entry(entity).State = EntityState.Modified;
            int result = _dbEntities.SaveChanges();
            return result != 0;
        }
    }
}
