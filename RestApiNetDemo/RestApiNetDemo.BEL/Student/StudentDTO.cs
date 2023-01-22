using System.ComponentModel.DataAnnotations;

namespace RestApiNetDemo.BEL.Student
{
    public class StudentDTO : CreateStudentDTO
    {
        public int Id { get; set; }
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        [Display(Name = "Student Name")]
        public string FullName => FirstName + " " + MiddleName + " " + LastName;

    }
}
