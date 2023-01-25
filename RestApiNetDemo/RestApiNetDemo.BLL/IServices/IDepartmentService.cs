using RestApiNetDemo.BEL.Department;
using RestApiNetDemo.BLL.Helpers;
using System.Collections.Generic;

namespace RestApiNetDemo.BLL.IServices
{
    public interface IDepartmentService
    {

        ServiceResponse GetDepartmentList();

        ServiceResponse GetDepartmentById(int id);

        //DepartmentDTO GetDepartmentByName(string name);


        ServiceResponse AddNewDepartment(CreateDepartmentDTO viewModel);


        ServiceResponse UpdateDepartment(DepartmentDTO viewModel);


        ServiceResponse DeleteDepartment(int id);
    }
}
