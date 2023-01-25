using RestApiNetDemo.BLL.IServices;
using System.Web.Http;

namespace RestApiNetDemo.Controllers
{
    public class TeacherController : ApiController
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }
    }
}
