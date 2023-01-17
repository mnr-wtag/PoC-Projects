using DotNetMvcDemo.Data;
using DotNetMvcDemo.Models;
using DotNetMvcDemo.Repository;
using DotNetMvcDemo.Services;
using DotNetMvcDemo.UnitOfWork;
using DotNetMvcDemo.ViewModels.Department;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace DotNetMvcDemo.Controllers
{

    public class DepartmentsController : Controller
    {
        private readonly UnitOfWork<ApplicationDbContext> _unitOfWork = new UnitOfWork<ApplicationDbContext>();
        private readonly GenericRepository<Department> _repository;
        public DepartmentsController()
        {
            _repository = new GenericRepository<Department>(_unitOfWork);
        }

        public ActionResult Index()
        {

            IEnumerable<Department> departments = _repository.GetAll();
            if (departments == null) return HttpNotFound();
            List<DepartmentViewModel> departmentsViewList = new List<DepartmentViewModel>();

            foreach (Department department in departments)
            {
                DepartmentViewModel departmentView = new DepartmentViewModel
                {
                    Id = department.Id,
                    Name = department.Name,
                    Description = department.Description
                };
                departmentsViewList.Add(departmentView);
            }

            return View(departmentsViewList);
        }


        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            DepartmentService service = new DepartmentService();
            DepartmentViewModel result = service.GetDepartmentById(id);
            return result == null ? HttpNotFound() : (ActionResult)View(result);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateDepartmentViewModel createDepartmentViewModel)
        {
            if (!ModelState.IsValid) return View(createDepartmentViewModel);
            DepartmentService service = new DepartmentService();
            bool result = service.AddNewDepartment(createDepartmentViewModel);
            return result ? RedirectToAction("Index") : (ActionResult)HttpNotFound();
        }


        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Department department = _repository.GetById(x => x.Id == id);
            DepartmentViewModel departmentView = new DepartmentViewModel();
            if (department == null) return View(departmentView);

            departmentView.Id = department.Id;
            departmentView.Name = department.Name;
            departmentView.Description = department.Description;

            return View(departmentView);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentViewModel departmentView)
        {
            if (!ModelState.IsValid) return View(departmentView);

            DepartmentService service = new DepartmentService();
            bool result = service.UpdateDepartment(departmentView);
            return result ? RedirectToAction("Index") : (ActionResult)View(departmentView);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DepartmentService service = new DepartmentService();
            bool result = service.DeleteDepartment(id);
            return RedirectToAction("Index");

        }
    }
}