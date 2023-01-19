using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using RestApiNetDemo.DAL.Repositories;
using System.Collections.Generic;

namespace RestApiNetDemo.Tests.DataAccessLayerTests.Repositories
{
    /// <summary>
    /// Summary description for CourseRepoTest
    /// </summary>
    [TestClass]
    public class CourseRepoTests
    {
        readonly Mock<IRepository<Cours, int>> mockRepository = new Mock<IRepository<Cours, int>>();
        private readonly CourseRepo courseRepo;
        //public CourseRepoTests()
        //{
        //    courseRepo = new CourseRepo(mockRepository.Object);
        //}

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get => testContextInstance;
            set => testContextInstance = value;
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetAll_ShouldReturnCourseList()
        {
            //Arrange
            var mockData = new List<Cours> { new Cours { Id = 1, Name = "Data Structure" } };

            mockRepository.Setup(r => r.GetAll()).Returns(mockData);

            //Act
            var actualData = courseRepo.GetAll();
            var expectedData = mockData;

            //Assert
            Assert.AreEqual(expectedData, actualData);
        }

        [TestMethod]
        public void GetById_ShouldReturnCourseById()
        {
            //Arrange
            var testId = 1;
            var testResult = new Cours { Id = 1, Name = "Data Structure" };

            mockRepository.Setup(r => r.GetById(testId)).Returns(testResult);

            //Act
            var actualData = courseRepo.GetById(1);
            var expectedData = new Cours { Id = 1, Name = "Data Structure" };

            //Assert
            Assert.AreEqual(expectedData, actualData);
        }
    }
}
