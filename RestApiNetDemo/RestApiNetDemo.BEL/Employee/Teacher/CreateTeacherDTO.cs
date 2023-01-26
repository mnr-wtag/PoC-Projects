using System.ComponentModel.DataAnnotations;

namespace RestApiNetDemo.BEL.Employee.Teacher
{
    public class CreateTeacherDTO : EmployeeDTO
    {
        public CreateTeacherDTO() : base()
        {
        }

        [RegularExpression(@"^\d{6}-\d{1}$", ErrorMessage = "The teacher ID card pattern should be like this i.e. 139022-3")]
        public string TeacherCardNumber { get; set; }

        public bool IsInProbation { get; set; } = false;
    }
}