using DotNetMvcDemo.Data;
using DotNetMvcDemo.Models;
using DotNetMvcDemo.Repository;
using DotNetMvcDemo.UnitOfWork;

namespace DotNetMvcDemo.Services
{
    public class EnrollmentService
    {
        private readonly UnitOfWork<ApplicationDbContext> _unitOfWork = new UnitOfWork<ApplicationDbContext>();
        private readonly IGenericRepository<Enrollment> _repository;


    }
}