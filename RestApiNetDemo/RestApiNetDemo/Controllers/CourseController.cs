using RestApiNetDemo.BLL.IServices;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestApiNetDemo.Controllers
{
    public class CourseController : ApiController
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [Route("api/courses")]
        [HttpGet]
        public HttpResponseMessage GetCourses()
        {
            try
            {
                var data = _courseService.GetCourseList();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception)
            {
                // throw;
                return Request.CreateResponse(HttpStatusCode.NotFound, "Not found");
            }
        }
    }
}