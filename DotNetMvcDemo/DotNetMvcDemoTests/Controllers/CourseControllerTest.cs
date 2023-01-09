using System;
using DotNetMvcDemo.Controllers;
using DotNetMvcDemo.Models;
using DotNetMvcDemo.Repository;
using DotNetMvcDemo.Services;
using DotNetMvcDemo.ViewModels.Course;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace DotNetMvcDemoTests.Controllers
{
    public class CourseControllerTest
    {
        public Mock<IGenericRepository<Course>> mock = new Mock<IGenericRepository<Course>>();
        public CoursesController _controller = new CoursesController();
        private readonly CourseService courseService;
        public CourseControllerTest()
        {
            courseService = new CourseService(mock.Object);
        }
       

        [Fact]
        public void Can_Get_Course()
        {
            //Arrange
            var courseId = 2;

            var courseName = "Test Course";
            var courseCredit = 3;
            var courseDepartmentId = 1;
            var courseCreatedAt = DateTime.Now;
            var courseCreatedBy = 1;
            var courseUpdatedAt = DateTime.Now;
            var courseUpdatedBy = 1;
            var mockData = new Course
            {
                Id = courseId,
                Name = courseName,
                Credit = courseCredit,
                DepartmentId = courseDepartmentId,
                CreatedAt = courseCreatedAt,
                CreatedBy = courseCreatedBy,
                UpdatedAt = courseUpdatedAt,
                UpdatedBy = courseUpdatedBy,
            };
            
            mock.Setup(x => x.GetById(a=>a.Id==courseId, null)).Returns(mockData);

            //Act
            var expectedData = courseService.GetCourseById(2);
            
            //Assertion
            Assert.Equal(expectedData.Name, courseName);
            Assert.Equal(expectedData.Credit,courseCredit);
        }


        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = _controller.Index() ;
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Can_Get_Course_List()
        {
            //Arrange
           
            var mockData = new List<Course>
            {
                new Course { Name = "Demo1" },
                new Course { Name = "Demo2" }
            };
            mock.Setup(x => x.GetAll(null, null, null))
                                                                .Returns(mockData);

            //Act
           
            var expectedData = courseService.GetCourseList().ToList();
           
            //Assertion

            Assert.Equal(expectedData.Count, mockData.Count);
            Assert.Equal(expectedData.First().Name, mockData.First().Name);
            Assert.Equal(expectedData.Last().Name, mockData.Last().Name);
        }

        [Fact]
        public void Create_InvalidModelState_ReturnsView()
        {
            _controller.ModelState.AddModelError("Name", @"Name is required");

            var course = new CreateCourseViewModel { Credit = 3 };

            var result = _controller.Create(course);

            var viewResult = Assert.IsType<ViewResult>(result);
            var testDept = Assert.IsType<CreateCourseViewModel>(viewResult.Model);

            Assert.Equal(course.Credit, testDept.Credit);

        }

        [Fact]
        public void Create_InvalidModelState_CreateCourseNeverExecutes()
        {
            _controller.ModelState.AddModelError("Name", @"Name is required");

            var course = new CreateCourseViewModel { Credit = 3 };

            _controller.Create(course);

            mock.Verify(x => x.Insert(It.IsAny<Course>()), Times.Never);
        }


        
    }
}
