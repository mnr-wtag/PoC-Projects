using RestApiNetDemo.BLL.IServices;
using System.Web.Http;

namespace RestApiNetDemo.Controllers
{
    public class StudentController : ApiController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
    }
}