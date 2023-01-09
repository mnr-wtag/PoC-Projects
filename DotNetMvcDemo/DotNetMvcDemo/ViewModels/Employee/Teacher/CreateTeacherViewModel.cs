using System.ComponentModel.DataAnnotations;

namespace DotNetMvcDemo.ViewModels.Employee.Teacher
{
    public class CreateTeacherViewModel : EmployeeViewModel
    {
        [RegularExpression(@"^\d{6}-\d{1}$", ErrorMessage = "The teacher ID card pattern should be like this i.e. 139022-3")]
        public string TeacherCardNumber { get; set; }

        public bool IsInProbation { get; set; } = false;
    }
}