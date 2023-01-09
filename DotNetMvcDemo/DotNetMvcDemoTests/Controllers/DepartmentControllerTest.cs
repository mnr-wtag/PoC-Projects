using System;
using System.Collections;
using DotNetMvcDemo.Controllers;
using DotNetMvcDemo.Models;
using DotNetMvcDemo.ViewModels;
using DotNetMvcDemo.ViewModels.Department;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using DotNetMvcDemo.Repository;
using DotNetMvcDemo.Services;
using FluentAssertions;
using Xunit;

namespace DotNetMvcDemoTests.Controllers
{
    public class DepartmentControllerTest
    {

        public Mock<IGenericRepository<Department>> mock = new Mock<IGenericRepository<Department>>();
        public readonly DepartmentService deptService;
        public DepartmentControllerTest()
        {
                deptService = new DepartmentService(mock.Object);
        }
        private static List<Department> GetTestDepartments()
        {
            var mockDepartments = new List<Department>
            {
                new Department
                {
                    Id = 1,
                    Name = "test one",
                    Description = "test desc"
                },
                new Department
                {
                    Id = 2,
                    Name = "test two",
                    Description = "test desc"
                },
                new Department
                {
                    Id = 3,
                    Name = "test three",
                    Description = "test desc"
                }
            };
            return mockDepartments;
        }

        [Fact]
        public void Can_Get_Department()
        {
            //Arrange
            var departmentId = 2;
            var departmentName = "CS";
            var departmentDescription = "Computer Science";
            var mockData = new Department
            {
                Id = departmentId,
                Name = departmentName,
                Description = departmentDescription
            };

            mock.Setup(x => x.GetById(a => a.Id == departmentId, null)).Returns(mockData);

            var expectedData = deptService.GetDepartmentById(2);

            //Assertion
            Assert.Equal(expectedData.Name, departmentName);
            Assert.Equal(expectedData.Description, departmentDescription);
        }
        //[Fact]
        //public void GetById_ShouldReturnNothing_WhenDepartmentDoesNotExist()
        //{
        //    mock.Setup( x=>x.GetById(It.Is<null>())).Returns(()=>null);
        //}

        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var controller = new DepartmentsController();
            var result = controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Can_Get_Department_List()
        {
            //Arrange

            var mockData = new List<Department>
            {
                new Department { Name = "CS" },
                new Department { Name = "EEE" }
            };
            mock.Setup(x => x.GetAll(null, null, null))
                                                                .Returns(mockData);

            //Act
           
            var service = new DepartmentService(mock.Object);
            var expectedData = service.GetDepartmentList().ToList();
            
            //Assertion

            Assert.Equal(expectedData.Count, mockData.Count);
            Assert.Equal(expectedData.First().Name, mockData.First().Name);
            Assert.Equal(expectedData.Last().Name, mockData.Last().Name);
        }

        [Fact]
        public void Create_InvalidModelState_ReturnsView()
        {
            var _controller = new DepartmentsController();
            _controller.ModelState.AddModelError("Name", @"Name is required");

            var dept = new CreateDepartmentViewModel { Description = "error khao" };

            var result = _controller.Create(dept);

            var viewResult = Assert.IsType<ViewResult>(result);
            var testDept = Assert.IsType<CreateDepartmentViewModel>(viewResult.Model);

            Assert.Equal(dept.Description, testDept.Description);

        }

        [Fact]
        public void Create_InvalidModelState_CreateDepartmentNeverExecutes()
        {
            var _controller = new DepartmentsController();
            _controller.ModelState.AddModelError("Name", "Name is required");

            var dept = new CreateDepartmentViewModel { Description = "error khao" };

            _controller.Create(dept);

            mock.Verify(x => x.Insert(It.IsAny<Department>()), Times.Never);
        }

        [Fact]
        public void Dummy_Test()
        {
            //Arrange
            var myTestString = "Hello, this is a test string";

            //Act
            myTestString.Should().StartWith("He").And.EndWith("g").And.HaveLength(28);

            //Assertion
            Assert.StartsWith("He", myTestString);
            Assert.EndsWith("g", myTestString);
            Assert.Equal(28, myTestString.Length);

        }
        [Fact]
        public void String_Assertion_Demo()
        {
            var myTestString = "hello, world";
            var stringSimilarToMyTestString = "HELLO, WORLD";
            var myEmptyString = "";
            string myNullString = null;

            myTestString.Should().Be("hello, world");
            myTestString.Should().BeEquivalentTo(stringSimilarToMyTestString );
            myNullString.Should().BeNull();
            myEmptyString.Should().BeEmpty();
            myEmptyString.Should().BeNullOrEmpty();
            myTestString.Should().BeLowerCased();
            stringSimilarToMyTestString.Should().BeUpperCased();
        }

        [Fact]
        public void Regex_Test()
        {
            var myTestString = "Hello, this is a test string";
            var myDateString = "05/30/2022";

            myTestString.Should().MatchEquivalentOf("HElLo, this is a * string");
            myDateString.Should().MatchRegex("\\d{1,2}\\/\\d{1,2}\\/\\d{4}");
        }

        
    }
}
