using RestApiNetDemo.BEL.Student;
using RestApiNetDemo.BLL.Helpers;

namespace RestApiNetDemo.BLL.IServices
{
    public interface IStudentService
    {
        ServiceResponse GetStudentList();

        ServiceResponse GetStudentById(int id);

        ServiceResponse GetStudentsByName(string name);

        ServiceResponse AddNewStudent(CreateStudentDTO viewModel);

        ServiceResponse UpdateDepartment(StudentDTO viewModel);

        ServiceResponse DeleteStudent(int id);
    }
}