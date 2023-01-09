using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DotNetMvcDemo.ViewModels.Student
{
    public class CreateStudentViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Display(Name = "Student ID Card No.")]
        [RegularExpression(@"^\d{2}-\d{5}-\d{1}$", ErrorMessage = "The student ID card pattern should be like this i.e. 18-39022-3")]
        public string StudentCardNumber { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Admission Date")]
        public DateTime EnrollmentDate { get; set; }

        public int DepartmentId { get; set; }

        public int? CourseId { get; set; }
        public SelectList CourseList { get; set; }

        public List<int> CourseIds { get; set; }

        public string Password { get; set; }
    }
}