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
            _dbEntities.Students.Add(entity);
            int result = _dbEntities.SaveChanges();
            return result != 0;
        }

        public bool Delete(int id)
        {
            Student student = _dbEntities.Students.FirstOrDefault(s => s.Id == id);
            if (student == null) return false;
            _dbEntities.Students.Remove(student);
            int result = _dbEntities.SaveChanges();
            return result != 0;
        }

        public List<Student> GetAll()
        {
            List<Student> studentList = _dbEntities.Students.ToList();
            return studentList;
        }

        public Student GetById(int id)
        {
            Student student = _dbEntities.Students.FirstOrDefault(s => s.Id == id);
            return student;
        }

        public bool Update(Student entity)
        {
            _dbEntities.Entry(entity).State = EntityState.Modified;
            int result = _dbEntities.SaveChanges();
            return result != 0;
        }
    }
}
