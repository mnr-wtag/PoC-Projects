using System.Collections.Generic;

namespace DotNetMvcDemo.ViewModels.Employee.Teacher
{
    public class TeacherDetailsViewModel : TeacherViewModel
    {
        public IEnumerable<Models.Department> Departments { get; set; }
    }
}