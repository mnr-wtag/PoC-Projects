using DotNetMvcDemo.Data;
using DotNetMvcDemo.Models;
using DotNetMvcDemo.Repository;
using DotNetMvcDemo.UnitOfWork;
using DotNetMvcDemo.ViewModels.Department;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetMvcDemo.Services
{
    public class DepartmentService
    {
        private readonly UnitOfWork<ApplicationDbContext> _unitOfWork = new UnitOfWork<ApplicationDbContext>();
        private readonly IGenericRepository<Department> _repository;

        public DepartmentService()
        {
            //If you want to use Generic Repository with Unit of work
            _repository = new GenericRepository<Department>(_unitOfWork);

        }

        public DepartmentService(IGenericRepository<Department> repository)
        {
            _repository = repository;
        }

        public IEnumerable<DepartmentViewModel> GetDepartmentList()
        {

            IEnumerable<Department> departments = _repository.GetAll();
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

            return departmentsViewList;
        }

        public DepartmentViewModel GetDepartmentById(int? id)
        {

            //var department = db.Departments.Find(id);
            Department department = _repository.GetById(x => x.Id == id);

            if (department == null) return null;

            DepartmentDetailsViewModel departmentView = new DepartmentDetailsViewModel
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description
            };

            ICollection<Teacher> departmentTeachers = department.Teachers;
            if (departmentTeachers != null) departmentView.DepartmentTeachers = departmentTeachers.ToList();

            return departmentView;

        }

        public DepartmentViewModel GetDepartmentByName(string name)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Department department = _repository.GetById(x => x.Name.Contains(name));
                DepartmentDetailsViewModel departmentView = new DepartmentDetailsViewModel();
                if (department == null) return null;

                departmentView.Id = department.Id;
                departmentView.Name = department.Name;
                departmentView.Description = department.Description;
                ICollection<Teacher> departmentTeachers = db.Departments.Where(x => x.Id == department.Id).Select(x => x.Teachers)
                    .FirstOrDefault();
                if (departmentTeachers != null) departmentView.DepartmentTeachers = departmentTeachers.ToList();

                return departmentView;
            }
        }

        public bool AddNewDepartment(CreateDepartmentViewModel viewModel)
        {
            try
            {
                Department department = new Department();
                department.Name = viewModel.Name;
                department.Description = viewModel.Description;
                department.CreatedAt = DateTime.Now;
                department.UpdatedAt = DateTime.Now;
                department.UpdatedBy = 1;
                department.CreatedBy = 1;

                _unitOfWork.CreateTransaction();

                _repository.Insert(department);
                _unitOfWork.Save();

                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                //Log the exception and rollback the transaction
                _unitOfWork.Rollback();
                throw;
            }
        }

        public bool UpdateDepartment(DepartmentViewModel viewModel)
        {
            if (viewModel == null) return false;
            Department model = _repository.GetById(x => x.Id == viewModel.Id);
            if (model == null) return false;
            model.Name = viewModel.Name;
            model.Description = viewModel.Description;
            model.UpdatedAt = DateTime.Now;
            model.UpdatedBy = 1;

            _repository.Update(model);
            _unitOfWork.Save();
            return true;
        }


        public bool DeleteDepartment(int? id)
        {
            Department department = _repository.GetById(x => x.Id == id);

            if (department == null) return false;
            _repository.Delete(department);
            _unitOfWork.Save();
            return true;
        }


    }
}