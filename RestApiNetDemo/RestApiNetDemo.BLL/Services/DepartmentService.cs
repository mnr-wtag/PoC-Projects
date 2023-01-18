using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using RestApiNetDemo.DAL.Repositories;

namespace RestApiNetDemo.BLL.Services
{
    public class DepartmentService
    {
        private readonly IRepository<Department, int> _repo;

        public DepartmentService(IRepository<Cours, int> repo)
        {
            _repo = repo;
        }

        public DepartmentService()
        {
            _repo = new DepartmentRepo();
        }
    }
}
