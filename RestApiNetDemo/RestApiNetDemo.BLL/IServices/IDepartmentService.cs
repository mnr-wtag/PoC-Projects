using RestApiNetDemo.BEL.Department;
using RestApiNetDemo.BLL.Helpers;

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