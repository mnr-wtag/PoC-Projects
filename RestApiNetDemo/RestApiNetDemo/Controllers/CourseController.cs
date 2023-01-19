using RestApiNetDemo.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestApiNetDemo.Controllers
{
    public class CourseController : ApiController
    {
        [Route("api/courses")]
        [HttpGet]
        public HttpResponseMessage GetCourses()
        {
			try
			{
                var service = new CourseService();
                var data = service.GetCourseList();
                return Request.CreateResponse(HttpStatusCode.OK, data);
			}
			catch (Exception)
			{
                throw;
                return Request.CreateResponse(HttpStatusCode.NotFound, "Not found");
            }
        }


    }
}
