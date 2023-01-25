using RestApiNetDemo.BLL.IServices;
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
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [Route("api/departments")]
        [HttpGet]
        public HttpResponseMessage GetDepartments()
        {
            try
            {
                var data = _departmentService.GetDepartmentList();
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
                var data = _departmentService.GetDepartmentById(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception)
            {
               
                return Request.CreateResponse(HttpStatusCode.NotFound, "Not found");
            }
        }

       

    }
}
