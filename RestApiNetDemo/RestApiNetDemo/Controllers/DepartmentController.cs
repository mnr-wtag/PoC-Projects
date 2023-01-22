using RestApiNetDemo.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestApiNetDemo.Controllers
{
    public class DepartmentController : ApiController
    {
        [Route("api/departments")]
        [HttpGet]
        public HttpResponseMessage GetDepartments()
        {
            try
            {
                var service = new DepartmentService();
                var data = service.GetDepartmentList();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Request.CreateResponse(HttpStatusCode.NotFound, "Not found");
            }
        }

        [Route("api/department/{id}")]
        [HttpGet]
        public HttpResponseMessage GetDepartmentById(int id)
        {
            try
            {
                var service = new DepartmentService();
                var data = service.GetDepartmentById(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception)
            {
               
                return Request.CreateResponse(HttpStatusCode.NotFound, "Not found");
            }
        }

       

    }
}
