using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using RestApiNetDemo.DAL.Repositories;

namespace RestApiNetDemo.DAL
{
    public class DataAccessFactory
    {
        private static readonly DotNetMvcDbEntities _dbEntities = new DotNetMvcDbEntities();

        public static IRepository<Student, int> StudentDataAccess()
        {
            return new StudentRepo(_dbEntities);
        }

        public static IRepository<Teacher, int> TeacherDataAccess()
        {
            return new TeacherRepo(_dbEntities);
        }

        public static IRepository<Admin, int> AdminDataAccess()
        {
            return new AdminRepo(_dbEntities);
        }

        public static IRepository<Cours, int> CourseDataAccess()
        {
            return new CourseRepo(_dbEntities);
        }

        public static IRepository<Department, int> DepartmentDataAccess()
        {
            return new DepartmentRepo(_dbEntities);
        }

        public static IBulkInsert<Enrollment> EnrollmentDataAccess()
        {
            return new EnrollmentRepo();
        }

        public static IRepository<AuthUser, int> AuthUserDataAccess()
        {
            return new AuthRepo();
        }
    }
}