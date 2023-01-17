using DotNetMvcDemo.Data;
using DotNetMvcDemo.Models;
using DotNetMvcDemo.Repository;
using DotNetMvcDemo.Services;
using DotNetMvcDemo.UnitOfWork;
using DotNetMvcDemo.ViewModels.Student;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DotNetMvcDemo.Controllers
{
    public class StudentsController : Controller
    {
        private readonly UnitOfWork<ApplicationDbContext> _unitOfWork = new UnitOfWork<ApplicationDbContext>();
        private readonly GenericRepository<Student> _repository;
        private readonly GenericRepository<Course> _courseRepository;
        private readonly GenericRepository<Department> _deptRepository;

        public StudentsController()
        {
            _repository = new GenericRepository<Student>(_unitOfWork);
            _courseRepository = new GenericRepository<Course>(_unitOfWork);
            _deptRepository = new GenericRepository<Department>(_unitOfWork);
        }

        public ActionResult Index()
        {
            StudentService service = new StudentService();
            System.Collections.Generic.IEnumerable<StudentViewModel> data = service.GetStudentList();
            if (data != null)
            {
                data = data.ToList();
                return View(data);
            }
            return View();
        }


        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            StudentService service = new StudentService();
            StudentDetailsViewModel data = service.GetStudentById(id);
            return data == null ? HttpNotFound() : (ActionResult)View(data);
        }

        [HttpGet]
        public JsonResult FetchCourses(int ID)
        {
            var data = _courseRepository.GetAll(x => x.DepartmentId == ID)
                                                             .Select(l => new { Value = l.Id, Text = l.Name });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        private void ConfigureViewModel(CreateStudentViewModel model)
        {
            if (model.DepartmentId != 0)
            {
                System.Collections.Generic.IEnumerable<Course> courses = _courseRepository.GetAll(x => x.DepartmentId == model.DepartmentId);
                model.CourseList = new SelectList(courses, "Id", "Name");
            }
            else
            {
                model.CourseList = new SelectList(Enumerable.Empty<SelectListItem>());
            }

        }
        public ActionResult Create(int? id)
        {
            ViewBag.DepartmentId = new SelectList(_deptRepository.GetAll(), "Id", "Name");
            CreateStudentViewModel viewModel = new CreateStudentViewModel();
            ConfigureViewModel(viewModel);
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateStudentViewModel createStudentViewModel)
        {
            if (ModelState.IsValid)
            {
                StudentService service = new StudentService();
                Helpers.ServiceResponse result = service.AddNewStudent(createStudentViewModel);
                if (result.Response == Helpers.Response.Success) return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(_deptRepository.GetAll(), "Id", "Name");
            ConfigureViewModel(createStudentViewModel);
            return View(createStudentViewModel);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Student student = _repository.GetById(x => x.Id == id);
            if (student == null) return HttpNotFound();

            StudentViewModel viewModel = new StudentViewModel();
            viewModel.Id = student.Id;
            viewModel.FirstName = student.FirstName;
            viewModel.MiddleName = student.MiddleName;
            viewModel.LastName = student.LastName;
            viewModel.DepartmentId = student.DepartmentId;
            viewModel.StudentCardNumber = student.StudentCardNumber;

            ViewBag.DepartmentId = new SelectList(_deptRepository.GetAll(), "Id", "Name", student.DepartmentId);
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            StudentService service = new StudentService();
            bool result = service.UpdateDepartment(viewModel);
            return result ? RedirectToAction("Index") : (ActionResult)View(viewModel);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            StudentService service = new StudentService();
            bool result = service.DeleteStudent(id);

            return result == false ? HttpNotFound() : (ActionResult)RedirectToAction("Index");
        }
    }
}