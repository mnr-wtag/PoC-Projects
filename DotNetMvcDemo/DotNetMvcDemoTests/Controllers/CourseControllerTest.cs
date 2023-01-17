using DotNetMvcDemo.Controllers;
using DotNetMvcDemo.Models;
using DotNetMvcDemo.Repository;
using DotNetMvcDemo.Services;
using DotNetMvcDemo.ViewModels.Course;
using Moq;
using System;
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
            int courseId = 2;

            string courseName = "Test Course";
            int courseCredit = 3;
            int courseDepartmentId = 1;
            DateTime courseCreatedAt = DateTime.Now;
            int courseCreatedBy = 1;
            DateTime courseUpdatedAt = DateTime.Now;
            int courseUpdatedBy = 1;
            Course mockData = new Course
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

            mock.Setup(x => x.GetById(a => a.Id == courseId, null)).Returns(mockData);

            //Act
            CourseDetailsViewModel expectedData = courseService.GetCourseById(2);

            //Assertion
            Assert.Equal(expectedData.Name, courseName);
            Assert.Equal(expectedData.Credit, courseCredit);
        }


        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            ActionResult result = _controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Can_Get_Course_List()
        {
            //Arrange

            List<Course> mockData = new List<Course>
            {
                new Course { Name = "Demo1" },
                new Course { Name = "Demo2" }
            };
            mock.Setup(x => x.GetAll(null, null, null))
                                                                .Returns(mockData);

            //Act

            List<CourseViewModel> expectedData = courseService.GetCourseList().ToList();

            //Assertion

            Assert.Equal(expectedData.Count, mockData.Count);
            Assert.Equal(expectedData.First().Name, mockData.First().Name);
            Assert.Equal(expectedData.Last().Name, mockData.Last().Name);
        }

        [Fact]
        public void Create_InvalidModelState_ReturnsView()
        {
            _controller.ModelState.AddModelError("Name", @"Name is required");

            CreateCourseViewModel course = new CreateCourseViewModel { Credit = 3 };

            ActionResult result = _controller.Create(course);

            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            CreateCourseViewModel testDept = Assert.IsType<CreateCourseViewModel>(viewResult.Model);

            Assert.Equal(course.Credit, testDept.Credit);

        }

        [Fact]
        public void Create_InvalidModelState_CreateCourseNeverExecutes()
        {
            _controller.ModelState.AddModelError("Name", @"Name is required");

            CreateCourseViewModel course = new CreateCourseViewModel { Credit = 3 };

            _controller.Create(course);

            mock.Verify(x => x.Insert(It.IsAny<Course>()), Times.Never);
        }



    }
}
