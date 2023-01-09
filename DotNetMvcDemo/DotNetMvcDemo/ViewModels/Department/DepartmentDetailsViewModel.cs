using DotNetMvcDemo.Models;
using System.Collections.Generic;

namespace DotNetMvcDemo.ViewModels.Department
{
    public class DepartmentDetailsViewModel : DepartmentViewModel
    {
        public List<Models.Teacher> DepartmentTeachers { get; set; }
        public List<Admin> DepartmentAdmins { get; set; }
    }
}