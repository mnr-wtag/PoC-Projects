using RestApiNetDemo.BEL.Department;
using RestApiNetDemo.BLL.IServices;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestApiNetDemo.Controllers
{
    [RoutePrefix("api/department")]
    public class DepartmentController : ApiController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public HttpResponseMessage GetDepartments()
        {
            try
            {
                var data = _departmentService.GetDepartmentList();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }

        [Route("{id}")]
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
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }

        [HttpPost]
        public HttpResponseMessage AddDepartment(CreateDepartmentDTO dto)
        {
            try
            {
                var response = _departmentService.AddNewDepartment(dto);
                return Request.CreateResponse(response.StatusCode, response.Message);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }
    }
}