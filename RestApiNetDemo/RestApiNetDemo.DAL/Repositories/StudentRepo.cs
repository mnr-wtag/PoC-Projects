using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RestApiNetDemo.DAL.Repositories
{
    internal class StudentRepo : IRepository<Student, int>
    {
        private readonly DotNetMvcDbEntities _dbEntities;
        public StudentRepo(DotNetMvcDbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public bool Add(Student entity)
        {
            try
            {
                _dbEntities.Students.Add(entity);
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
                Student student = _dbEntities.Students.FirstOrDefault(s => s.Id == id);
                if (student == null) return false;
                _dbEntities.Students.Remove(student);
                int result = _dbEntities.SaveChanges();
                return result != 0;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public List<Student> GetAll()
        {
            try
            {
                List<Student> studentList = _dbEntities.Students.ToList();
                return studentList;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public Student GetById(int id)
        {
            try
            {
                Student student = _dbEntities.Students.FirstOrDefault(s => s.Id == id);
                return student;
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public bool Update(Student entity)
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
