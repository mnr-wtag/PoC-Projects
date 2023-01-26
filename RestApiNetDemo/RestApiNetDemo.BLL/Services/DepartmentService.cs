using RestApiNetDemo.BEL.Department;
using RestApiNetDemo.BEL.Employee.Teacher;
using RestApiNetDemo.BLL.Helpers;
using RestApiNetDemo.BLL.IServices;
using RestApiNetDemo.DAL;
using RestApiNetDemo.DAL.Data;
using RestApiNetDemo.DAL.IRepositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;

namespace RestApiNetDemo.BLL.Services
{
    public class DepartmentService : IDepartmentService
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

        public ServiceResponse GetDepartmentList()
        {
            try
            {
                IEnumerable<Department> departments = _repo.GetAll();
                if (departments == null) return new ServiceResponse(HttpStatusCode.NotFound, "No department was found");
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

                return new ServiceResponse(HttpStatusCode.OK, "", departmentsViewList);
            }
            catch (Exception ex)
            {
                Log.Error($"{DateTime.UtcNow} - Class:{nameof(DepartmentService)} - Method:{nameof(GetDepartmentList)} --- Exception:{ex}");
                return new ServiceResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }

        public ServiceResponse GetDepartmentById(int id)
        {
            try
            {
                Department department = _repo.GetById(x => x.Id == id, new List<string> { "Teachers" });

                if (department == null) return new ServiceResponse(HttpStatusCode.NotFound, "Department not found");

                DepartmentDetailsDTO departmentView = new DepartmentDetailsDTO
                {
                    Id = department.Id,
                    Name = department.Name,
                    Description = department.Description
                };

                var departmentTeachers = department.Teachers;
                if (departmentTeachers != null) departmentView.DepartmentTeachers = (ICollection<TeacherDTO>)departmentTeachers;

                return new ServiceResponse(HttpStatusCode.OK, "", departmentView);
            }
            catch (Exception ex)
            {
                Log.Error($"{DateTime.UtcNow}  --- Class: {nameof(DepartmentService)}  --- Method: {nameof(GetDepartmentById)}  --- Exception:{ex}");
                return new ServiceResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
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

        public ServiceResponse AddNewDepartment(CreateDepartmentDTO viewModel)
        {
            try
            {
                if (viewModel == null) return new ServiceResponse(HttpStatusCode.BadRequest, "Department cannot be null");
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
                return result
                    ? new ServiceResponse(HttpStatusCode.Created, $"Department created successfully")
                    : new ServiceResponse(HttpStatusCode.BadRequest, "Department create failed");
            }
            catch (Exception ex)
            {
                Log.Error($"{DateTime.UtcNow} --- Class:{nameof(DepartmentService)}  --- Method: {nameof(AddNewDepartment)} --- Exception:{ex}");
                return new ServiceResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }

        public ServiceResponse UpdateDepartment(DepartmentDTO viewModel)
        {
            try
            {
                if (viewModel == null) return new ServiceResponse(HttpStatusCode.BadRequest, "Department cannot be null");
                Department model = _repo.GetById(x => x.Id == viewModel.Id);
                if (model == null) return new ServiceResponse(HttpStatusCode.NotFound, "Department not found");
                model.Name = viewModel.Name;
                model.Description = viewModel.Description;
                model.UpdatedAt = DateTime.Now;
                model.UpdatedBy = 1;

                var result = _repo.Update(model);
                return result
                    ? new ServiceResponse(HttpStatusCode.NoContent, "Course updated successfully")
                    : new ServiceResponse(HttpStatusCode.BadRequest, "Course update failed");
            }
            catch (Exception ex)
            {
                Log.Error($"{DateTime.UtcNow} --- Class:{nameof(DepartmentService)} --- Method:{nameof(UpdateDepartment)} --- Exception:{ex}");
                var _response = new ServiceResponse(HttpStatusCode.InternalServerError, "Something went wrong");
                return _response;
            }
        }

        public ServiceResponse DeleteDepartment(int id)
        {
            try
            {
                Department department = _repo.GetById(x => x.Id == id);

                if (department == null) return new ServiceResponse(HttpStatusCode.NotFound, "Department not found");
                var result = _repo.Delete(id);
                return result
                    ? new ServiceResponse(HttpStatusCode.NoContent, "Course deleted successfully")
                    : new ServiceResponse(HttpStatusCode.BadRequest, "Course delete failed");
            }
            catch (Exception ex)
            {
                Log.Error($"{DateTime.UtcNow} --- Class:{nameof(DepartmentService)} --- Method:{nameof(DeleteDepartment)} --- Exception:{ex}");
                return new ServiceResponse(HttpStatusCode.InternalServerError, "Something went wrong");
            }
        }
    }
}