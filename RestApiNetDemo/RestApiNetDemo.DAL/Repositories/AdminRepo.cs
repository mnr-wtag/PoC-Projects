using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RestApiNetDemo.DAL.Repositories
{
    internal class AdminRepo : IRepository<Admin, int>
    {
        private readonly DotNetMvcDbEntities _dbEntities;
        public AdminRepo(DotNetMvcDbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public bool Add(Admin entity)
        {
            try
            {
                _dbEntities.Admins.Add(entity);
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
                Admin admin = _dbEntities.Admins.FirstOrDefault(s => s.Id == id);
                if (admin == null) return false;
                _dbEntities.Admins.Remove(admin);
                int result = _dbEntities.SaveChanges();
                return result != 0;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public List<Admin> GetAll()
        {
            try
            {
                List<Admin> adminList = _dbEntities.Admins.ToList();
                return adminList;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public Admin GetById(int id)
        {
            try
            {
                Admin admin = _dbEntities.Admins.FirstOrDefault(s => s.Id == id);
                return admin;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public bool Update(Admin entity)
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
