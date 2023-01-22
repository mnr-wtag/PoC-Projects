using RestApiNetDemo.BEL.Department;
using RestApiNetDemo.BEL.Employee.Teacher;
using RestApiNetDemo.DAL;
using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using System;
using System.Collections.Generic;

namespace RestApiNetDemo.BLL.Services
{
    public class DepartmentService
    {
        private readonly IRepository<Department, int> _repo;

        public DepartmentService(IRepository<Department, int> repo)
        {
            _repo = repo;
        }

        public DepartmentService()
        {
            _repo = DataAccessFactory.DepartmentDataAccess();
        }


        public IEnumerable<DepartmentDTO> GetDepartmentList()
        {

            IEnumerable<Department> departments = _repo.GetAll();
            List<DepartmentDTO> departmentsViewList = new List<DepartmentDTO>();

            foreach (Department department in departments)
            {
                DepartmentDTO departmentView = new DepartmentDTO
                {
                    Id = department.Id,
                    Name = department.Name,
                    Description = department.Description
                };

                departmentsViewList.Add(departmentView);
            }

            return departmentsViewList;
        }

        public DepartmentDTO GetDepartmentById(int id)
        {
            Department department = _repo.GetById(x => x.Id == id);

            if (department == null) return null;

            DepartmentDetailsDTO departmentView = new DepartmentDetailsDTO
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description
            };

            var departmentTeachers = department.Teachers;
            if (departmentTeachers != null) departmentView.DepartmentTeachers = (ICollection<TeacherDTO>)departmentTeachers;

            return departmentView;

        }

        //public DepartmentDTO GetDepartmentByName(string name)
        //{
        //    using (DotNetMvcDbEntities db = new DotNetMvcDbEntities())
        //    {
        //        Department department = _repo.(name);
        //        DepartmentDetailsDTO departmentView = new DepartmentDetailsDTO();
        //        if (department == null) return null;

        //        departmentView.Id = department.Id;
        //        departmentView.Name = department.Name;
        //        departmentView.Description = department.Description;
        //        ICollection<Teacher> departmentTeachers = db.Departments.Where(x => x.Id == department.Id).Select(x => x.Teachers)
        //            .FirstOrDefault();
        //        if (departmentTeachers != null) departmentView.DepartmentTeachers = departmentTeachers.ToList();

        //        return departmentView;
        //    }
        //}

        public bool AddNewDepartment(CreateDepartmentDTO viewModel)
        {
            try
            {
                Department department = new Department
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = 1,
                    CreatedBy = 1
                };

                var result = _repo.Add(department);

                return result;
            }
            catch (Exception)
            {
                //Log the exception and rollback the transaction

                throw;
            }
        }

        public bool UpdateDepartment(DepartmentDTO viewModel)
        {
            if (viewModel == null) return false;
            Department model = _repo.GetById(x => x.Id == viewModel.Id);
            if (model == null) return false;
            model.Name = viewModel.Name;
            model.Description = viewModel.Description;
            model.UpdatedAt = DateTime.Now;
            model.UpdatedBy = 1;

            var result = _repo.Update(model);
            return result;
        }


        public bool DeleteDepartment(int id)
        {
            Department department = _repo.GetById(x => x.Id == id);

            if (department == null) return false;
            var result = _repo.Delete(id);
            return result;
        }
    }
}
