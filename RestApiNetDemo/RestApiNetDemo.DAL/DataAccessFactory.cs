using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using RestApiNetDemo.DAL.Repositories;

namespace RestApiNetDemo.DAL
{
    internal class DataAccessFactory
    {
        private static readonly DotNetMvcDbEntities _dbEntities = new DotNetMvcDbEntities();

        public static IRepository<Student, int> StudentDataAccess()
        {
            return new StudentRepo(_dbEntities);
        }
    }
}
