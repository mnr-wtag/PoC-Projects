using AutoMapper;
using DotNetMvcDemo.Models;
using DotNetMvcDemo.ViewModels.Department;
using DotNetMvcDemo.ViewModels.Student;

namespace DotNetMvcDemo.Configurations
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            CreateMap<Department, CreateDepartmentViewModel>().ReverseMap();
            CreateMap<Department, DepartmentViewModel>().ReverseMap();
            CreateMap<Department, DepartmentDetailsViewModel>().ReverseMap();

            CreateMap<Student, CreateStudentViewModel>().ReverseMap();
            CreateMap<Student, StudentViewModel>().ReverseMap();
            CreateMap<Student, StudentDetailsViewModel>().ReverseMap();


        }
    }
}