using Moq;
using RestApiNetDemo.BLL.Services;
using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;

namespace RestApiNetDemo.Tests.BusinessLogicLayerTests.Services
{
    internal class CourseServiceTests
    {
        private readonly Mock<IRepository<Cours, int>> Mock = new Mock<IRepository<Cours, int>>();
        private readonly CourseService _courseService;

        public CourseServiceTests()
        {
            _courseService = new CourseService(Mock.Object);
        }
    }
}