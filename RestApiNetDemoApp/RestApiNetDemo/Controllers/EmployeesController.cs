using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RestApiNetDemo.DAL;
using RestApiNetDemo.Repository;

namespace RestApiNetDemo.Controllers
{
    public class EmployeesController : ApiController
    {
        private EmployeeDBEntities db = new EmployeeDBEntities();
        private readonly IGenericRepository<Employee> repository = null;
        private readonly IEmployeeRepository employeeRepository = null;

        public EmployeesController()
        {
            this.employeeRepository = new EmployeeRepository();
            this.repository = new GenericRepository<Employee>();   
        }

        public EmployeesController(EmployeeRepository repository)
        {
            this.employeeRepository=repository;
        }

        public EmployeesController(GenericRepository<Employee> repository)
        {
            this.repository = repository;
        }

        // GET: api/Employees
        [HttpGet]
        public IHttpActionResult GetEmployees()
        {
            var employees = employeeRepository.GetAll();
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }

        // GET: api/Employees/5
        [ResponseType(typeof(Employee))]
        [HttpGet]
        public IHttpActionResult GetEmployee(int id)
        {
            var employee = employeeRepository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult EditEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }
            repository.Update(employee);
            //db.Entry(employee).State = EntityState.Modified;

            try
            {
                repository.Save();
                //db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employees
        [ResponseType(typeof(Employee))]
        [HttpPost]
        public IHttpActionResult AddEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            repository.Insert(employee);
            repository.Save();

            //db.Employees.Add(employee);
            //db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeID }, employee);
        }

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employee))]
        [HttpDelete]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee employee = repository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }

            repository.Delete(employee);
            repository.Save();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeID == id) > 0;
        }
    }
}