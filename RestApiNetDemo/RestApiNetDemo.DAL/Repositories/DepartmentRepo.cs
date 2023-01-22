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
            try
            {
                _dbEntities.Departments.Add(entity);
                int result = _dbEntities.SaveChanges();
                return result != 0;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public bool Delete(int id)
        {
            try
            {
                Department department = _dbEntities.Departments.FirstOrDefault(s => s.Id == id);
                if (department == null) return false;
                _dbEntities.Departments.Remove(department);
                int result = _dbEntities.SaveChanges();
                return result != 0;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public List<Department> GetAll()
        {
            try
            {
                List<Department> departmentList = _dbEntities.Departments.ToList();
                return departmentList;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public Department GetById(int id)
        {
            try
            {
                Department department = _dbEntities.Departments.FirstOrDefault(s => s.Id == id);
                return department;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public List<Department> Search(string search)
        {
            try
            {
                var department = _dbEntities.Departments.Where(s => s.Name.Contains(search)).ToList();
                return department;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public bool Update(Department entity)
        {
            try
            {
                _dbEntities.Entry(entity).State = EntityState.Modified;
                int result = _dbEntities.SaveChanges();
                return result != 0;
            }
            catch (System.Exception)
            {

                throw;
            }

        }
    }
}
