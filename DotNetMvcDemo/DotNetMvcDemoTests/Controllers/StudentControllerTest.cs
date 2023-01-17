namespace DotNetMvcDemoTests.Controllers
{
    //public class StudentControllerTest
    //{
    //    public Mock<IGenericRepository<Student>> mock = new Mock<IGenericRepository<Student>>();
    //    public readonly StudentService studentService;
    //    public StudentControllerTest()
    //    {
    //        studentService = new StudentService(mock.Object);
    //    }


    //    [Fact]
    //    public void Can_Get_Student()
    //    {
    //        //Arrange
    //        var studentId = 2;
    //        var studentName = "CS";

    //        var mockData = new Student
    //        {
    //            Id = studentId,
    //            FirstName = studentName,

    //        };

    //        mock.Setup(x => x.GetById(a => a.Id == studentId, null)).Returns(mockData);

    //        var expectedData = studentService.GetStudentById(2);

    //        //Assertion
    //        Assert.Equal(expectedData.Name, studentName);
    //        Assert.Equal(expectedData.Description, studentDescription);
    //    }
    //    //[Fact]
    //    //public void GetById_ShouldReturnNothing_WhenStudentDoesNotExist()
    //    //{
    //    //    mock.Setup( x=>x.GetById(It.Is<null>())).Returns(()=>null);
    //    //}

    //    [Fact]
    //    public void Index_ActionExecutes_ReturnsViewForIndex()
    //    {
    //        var controller = new StudentsController();
    //        var result = controller.Index();
    //        Assert.IsType<ViewResult>(result);
    //    }

    //    [Fact]
    //    public void Can_Get_Student_List()
    //    {
    //        //Arrange

    //        var mockData = new List<Student>
    //        {
    //            new Student { Name = "CS" },
    //            new Student { Name = "EEE" }
    //        };
    //        mock.Setup(x => x.GetAll(null, null, null))
    //                                                            .Returns(mockData);

    //        //Act

    //        var service = new StudentService(mock.Object);
    //        var expectedData = service.GetStudentList().ToList();

    //        //Assertion

    //        Assert.Equal(expectedData.Count, mockData.Count);
    //        Assert.Equal(expectedData.First().Name, mockData.First().Name);
    //        Assert.Equal(expectedData.Last().Name, mockData.Last().Name);
    //    }

    //    [Fact]
    //    public void Create_InvalidModelState_ReturnsView()
    //    {
    //        var _controller = new StudentsController();
    //        _controller.ModelState.AddModelError("Name", @"Name is required");

    //        var student = new CreateStudentViewModel { Description = "error khao" };

    //        var result = _controller.Create(student);

    //        var viewResult = Assert.IsType<ViewResult>(result);
    //        var testDept = Assert.IsType<CreateStudentViewModel>(viewResult.Model);

    //        Assert.Equal(student.Description, testDept.Description);

    //    }

    //    [Fact]
    //    public void Create_InvalidModelState_CreateStudentNeverExecutes()
    //    {
    //        var _controller = new StudentsController();
    //        _controller.ModelState.AddModelError("Name", "Name is required");

    //        var student = new CreateStudentViewModel { Description = "error khao" };

    //        _controller.Create(student);

    //        mock.Verify(x => x.Insert(It.IsAny<Student>()), Times.Never);
    //    }
    //}
}
