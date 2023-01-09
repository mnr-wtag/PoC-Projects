using System.ComponentModel.DataAnnotations;

namespace DotNetMvcDemo.ViewModels.Student
{
    public class StudentViewModel : CreateStudentViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        [Display(Name = "Student Name")]
        public string FullName => FirstName + " " + MiddleName + " " + LastName;


    }
}