using DotNetMvcDemo.Data;
using DotNetMvcDemo.Models;
using DotNetMvcDemo.Repository;
using DotNetMvcDemo.Services;
using DotNetMvcDemo.UnitOfWork;
using DotNetMvcDemo.ViewModels.Course;
using System.Net;
using System.Web.Mvc;

namespace DotNetMvcDemo.Controllers
{
    public class CoursesController : Controller
    {
        private readonly UnitOfWork<ApplicationDbContext> _unitOfWork = new UnitOfWork<ApplicationDbContext>();
        private readonly GenericRepository<Course> _repository;
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        public CoursesController()
        {
            _repository = new GenericRepository<Course>(_unitOfWork);
        }
        public ActionResult Index()
        {
            var service = new CourseService();
            var data = service.GetCourseList();
            return data == null ? HttpNotFound() : (ActionResult)View(data);
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var service = new CourseService();
            var data = service.GetCourseById(id);
            return data == null ? HttpNotFound() : (ActionResult)View(data);
        }

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(_db.Departments, "Id", "Name", "Select Department");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCourseViewModel createCourseViewModel)
        {
            if (ModelState.IsValid)
            {
                var service = new CourseService();
                var result = service.AddNewCourse(createCourseViewModel);
                if (result) return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(_db.Departments, "Id", "Name", createCourseViewModel.DepartmentId);

            return View(createCourseViewModel);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var course = _repository.GetById(x => x.Id == id);
            if (course == null) return HttpNotFound();

            var courseView = new CourseViewModel
            {
                Name = course.Name,
                Credit = course.Credit,
                Id = course.Id
            };
            ViewBag.DepartmentId = new SelectList(_db.Departments, "Id", "Name", courseView.DepartmentId);
            return View(courseView);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseViewModel courseView)
        {
            if (ModelState.IsValid)
            {
                var service = new CourseService();
                var result = service.UpdateCourse(courseView);
                if (result) return RedirectToAction("Index");
            }


            ViewBag.DepartmentId = new SelectList(_db.Departments, "Id", "Name", courseView.DepartmentId);


            return View(courseView);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var course = _repository.GetById(x => x.Id == id);
            if (course == null) return HttpNotFound();
            _repository.Delete(course);

            return RedirectToAction("Index");
        }


    }
}