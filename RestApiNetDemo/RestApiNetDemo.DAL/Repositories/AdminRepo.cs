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
            _dbEntities.Admins.Add(entity);
            int result = _dbEntities.SaveChanges();
            return result != 0;
        }

        public bool Delete(int id)
        {
            Admin admin = _dbEntities.Admins.FirstOrDefault(s => s.Id == id);
            if (admin == null) return false;
            _dbEntities.Admins.Remove(admin);
            int result = _dbEntities.SaveChanges();
            return result != 0;
        }

        public List<Admin> GetAll()
        {
            List<Admin> adminList = _dbEntities.Admins.ToList();
            return adminList;
        }

        public Admin GetById(int id)
        {
            Admin admin = _dbEntities.Admins.FirstOrDefault(s => s.Id == id);
            return admin;
        }

        public bool Update(Admin entity)
        {
            _dbEntities.Entry(entity).State = EntityState.Modified;
            int result = _dbEntities.SaveChanges();
            return result != 0;
        }
    }
}
