using DotNetMvcDemo.Data;
using DotNetMvcDemo.Models;
using DotNetMvcDemo.Repository;
using DotNetMvcDemo.Services;
using DotNetMvcDemo.UnitOfWork;
using DotNetMvcDemo.ViewModels.Employee.Teacher;
using System.Net;
using System.Web.Mvc;

namespace DotNetMvcDemo.Controllers
{
    public class TeachersController : Controller
    {
        private readonly UnitOfWork<ApplicationDbContext> _unitOfWork = new UnitOfWork<ApplicationDbContext>();
        private readonly GenericRepository<Teacher> _repository;
        private readonly ApplicationDbContext _db = new ApplicationDbContext();


        public ActionResult Index()
        {
            //IQueryable<Teacher> teachers = db.Teachers.Include(t => t.Department);
            //return View(teachers.ToList());
            var service = new TeacherService();
            var data = service.GetTeacherList();
            return data == null ? View() : (ActionResult)View(data);
        }


        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var service = new TeacherService();
            var data = service.GetTeacherById(id);
            return data == null ? HttpNotFound() : (ActionResult)View(data);
        }


        public ActionResult Create()
        {
            //ViewBag.DepartmentId = new SelectList(_db.Departments, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTeacherViewModel createTeacher)
        {
            if (ModelState.IsValid)
            {
                var service = new TeacherService();
                var result = service.AddNewTeacher(createTeacher);
                if (result) return RedirectToAction("Index");
            }
            return View(createTeacher);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var teacher = _repository.GetById(x => x.Id == id);
            if (teacher == null) return HttpNotFound();
            var viewModel = new TeacherViewModel();
            viewModel.Id = teacher.Id;
            viewModel.FirstName = teacher.FirstName;
            viewModel.LastName = teacher.LastName;
            viewModel.Salary = teacher.Salary;
            viewModel.IsInProbation = teacher.IsInProbation;
            viewModel.TeacherCardNumber = teacher.TeacherCardNumber;
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeacherViewModel teacher)
        {
            if (ModelState.IsValid)
            {
                var service = new TeacherService();
                var result = service.UpdateTeacher(teacher);
                if (result) return RedirectToAction("Index");
            }

            //ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", teacher.DepartmentId);
            return View(teacher);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var service = new TeacherService();
            var result = service.DeleteTeacher(id);
            return result ? RedirectToAction("Index") : (ActionResult)View();
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing) _db.Dispose();
            base.Dispose(disposing);
        }
    }
}