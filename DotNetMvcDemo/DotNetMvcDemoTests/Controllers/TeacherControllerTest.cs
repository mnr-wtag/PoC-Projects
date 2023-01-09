using DotNetMvcDemo.Controllers;
using DotNetMvcDemo.Models;
using DotNetMvcDemo.Repository;
using DotNetMvcDemo.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DotNetMvcDemo.ViewModels.Employee.Teacher;
using Xunit;

namespace DotNetMvcDemoTests.Controllers
{
    //public class TeacherControllerTest
    //{
    //    public Mock<IGenericRepository<Teacher>> mock = new Mock<IGenericRepository<Teacher>>();
    //    public readonly TeacherService teacherService;
    //    public TeacherControllerTest()
    //    {
    //            teacherService = new TeacherService(mock.Object);
    //    }
        
    //    [Fact]
    //    public void Can_Get_Teacher()
    //    {
    //        //Arrange
    //        var teacherId = 2;
    //        var teacherFirstName = "First";
    //        var teacherLastName = "Last";
            
    //        var mockData = new Teacher
    //        {
    //            Id = teacherId,
    //            FirstName = teacherFirstName,
    //            LastName = teacherLastName
    //        };

    //        mock.Setup(x => x.GetById(a => a.Id == teacherId, null)).Returns(mockData);

    //        var expectedData = teacherService.GetTeacherById(2);

    //        //Assertion
    //        Assert.Equal(expectedData.FirstName, teacherFirstName);
    //        Assert.Equal(expectedData.LastName, teacherLastName);
    //    }
    //    //[Fact]
    //    //public void GetById_ShouldReturnNothing_WhenTeacherDoesNotExist()
    //    //{
    //    //    mock.Setup( x=>x.GetById(It.Is<null>())).Returns(()=>null);
    //    //}

    //    [Fact]
    //    public void Index_ActionExecutes_ReturnsViewForIndex()
    //    {
    //        var controller = new TeachersController();
    //        var result = controller.Index();
    //        Assert.IsType<ViewResult>(result);
    //    }

    //    [Fact]
    //    public void Can_Get_Teacher_List()
    //    {
    //        //Arrange

    //        var mockData = new List<Teacher>
    //        {
    //            new Teacher { Name = "CS" },
    //            new Teacher { Name = "EEE" }
    //        };
    //        mock.Setup(x => x.GetAll(null, null, null))
    //                                                            .Returns(mockData);

    //        //Act
           
    //        var service = new TeacherService(mock.Object);
    //        var expectedData = service.GetTeacherList().ToList();
            
    //        //Assertion

    //        Assert.Equal(expectedData.Count, mockData.Count);
    //        Assert.Equal(expectedData.First().Name, mockData.First().Name);
    //        Assert.Equal(expectedData.Last().Name, mockData.Last().Name);
    //    }

    //    [Fact]
    //    public void Create_InvalidModelState_ReturnsView()
    //    {
    //        var _controller = new TeachersController();
    //        _controller.ModelState.AddModelError("Name", @"Name is required");

    //        var teacher = new CreateTeacherViewModel { Description = "error khao" };

    //        var result = _controller.Create(teacher);

    //        var viewResult = Assert.IsType<ViewResult>(result);
    //        var testDept = Assert.IsType<CreateTeacherViewModel>(viewResult.Model);

    //        Assert.Equal(teacher.Description, testDept.Description);

    //    }

    //    [Fact]
    //    public void Create_InvalidModelState_CreateTeacherNeverExecutes()
    //    {
    //        var _controller = new TeachersController();
    //        _controller.ModelState.AddModelError("Name", "Name is required");

    //        var teacher = new CreateTeacherViewModel { Description = "error khao" };

    //        _controller.Create(teacher);

    //        mock.Verify(x => x.Insert(It.IsAny<Teacher>()), Times.Never);
    //    }
    //}
}
