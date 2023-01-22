using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RestApiNetDemo.DAL.Repositories
{
    internal class TeacherRepo : IRepository<Teacher, int>
    {
        private readonly DotNetMvcDbEntities _dbEntities;
        public TeacherRepo(DotNetMvcDbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public bool Add(Teacher entity)
        {
            try
            {
                _dbEntities.Teachers.Add(entity);
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
                Teacher teacher = _dbEntities.Teachers.FirstOrDefault(s => s.Id == id);
                if (teacher == null) return false;
                _dbEntities.Teachers.Remove(teacher);
                int result = _dbEntities.SaveChanges();
                return result != 0;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public List<Teacher> GetAll()
        {
            try
            {
                List<Teacher> teacherList = _dbEntities.Teachers.ToList();
                return teacherList;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public Teacher GetById(int id)
        {
            try
            {
                Teacher teacher = _dbEntities.Teachers.FirstOrDefault(s => s.Id == id);
                return teacher;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public bool Update(Teacher entity)
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
