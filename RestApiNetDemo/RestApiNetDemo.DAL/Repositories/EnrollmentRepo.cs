using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiNetDemo.DAL.Repositories
{
    internal class EnrollmentRepo : IRepository<Enrollment,int>
    {
        public bool Add(Enrollment entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int entity)
        {
            throw new NotImplementedException();
        }

        public List<Enrollment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Enrollment GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Enrollment entity)
        {
            throw new NotImplementedException();
        }
    }
}
