namespace DotNetMvcDemoTests.Controllers
{
    //public class AdminControllerTest
    //{
    //    public Mock<IGenericRepository<Admin>> mock = new Mock<IGenericRepository<Admin>>();
    //    public readonly AdminService adminService;
    //    public AdminControllerTest()
    //    {
    //            adminService = new AdminService(mock.Object);
    //    }


    //    [Fact]
    //    public void Can_Get_Admin()
    //    {
    //        //Arrange
    //        var adminId = 2;
    //        var adminFirstName = "DemoFrist";
    //        var adminLastName = "demoLast";

    //        var mockData = new Admin
    //        {
    //            Id = adminId,
    //            FirstName = adminFirstName,
    //            LastName = adminLastName,
    //        };

    //        mock.Setup(x => x.GetById(a => a.Id == adminId, null)).Returns(mockData);

    //        var expectedData = adminService.GetAdminById(2);

    //        //Assertion
    //        Assert.Equal(expectedData., adminFirstName);
    //        Assert.Equal(expectedData.Description, adminDescription);
    //    }
    //    //[Fact]
    //    //public void GetById_ShouldReturnNothing_WhenAdminDoesNotExist()
    //    //{
    //    //    mock.Setup( x=>x.GetById(It.Is<null>())).Returns(()=>null);
    //    //}

    //    [Fact]
    //    public void Index_ActionExecutes_ReturnsViewForIndex()
    //    {
    //        var controller = new AdminsController();
    //        var result = controller.Index();
    //        Assert.IsType<ViewResult>(result);
    //    }

    //    [Fact]
    //    public void Can_Get_Admin_List()
    //    {
    //        //Arrange

    //        var mockData = new List<Admin>
    //        {
    //            new Admin { Name = "CS" },
    //            new Admin { Name = "EEE" }
    //        };
    //        mock.Setup(x => x.GetAll(null, null, null))
    //                                                            .Returns(mockData);

    //        //Act

    //        var service = new AdminService(mock.Object);
    //        var expectedData = service.GetAdminList().ToList();

    //        //Assertion

    //        Assert.Equal(expectedData.Count, mockData.Count);
    //        Assert.Equal(expectedData.First().Name, mockData.First().Name);
    //        Assert.Equal(expectedData.Last().Name, mockData.Last().Name);
    //    }

    //    [Fact]
    //    public void Create_InvalidModelState_ReturnsView()
    //    {
    //        var _controller = new AdminsController();
    //        _controller.ModelState.AddModelError("Name", @"Name is required");

    //        var admin = new CreateAdminViewModel { Description = "error khao" };

    //        var result = _controller.Create(admin);

    //        var viewResult = Assert.IsType<ViewResult>(result);
    //        var testDept = Assert.IsType<CreateAdminViewModel>(viewResult.Model);

    //        Assert.Equal(admin.Description, testDept.Description);

    //    }

    //    [Fact]
    //    public void Create_InvalidModelState_CreateAdminNeverExecutes()
    //    {
    //        var _controller = new AdminsController();
    //        _controller.ModelState.AddModelError("Name", "Name is required");

    //        var admin = new CreateAdminViewModel { Description = "error khao" };

    //        _controller.Create(admin);

    //        mock.Verify(x => x.Insert(It.IsAny<Admin>()), Times.Never);
    //    }
    //}
}
