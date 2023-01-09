using DotNetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace DotNetMvcDemo.Data
{
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var authUser = new AuthUser
            {
                UserName = "admin",
                Password = "admin",
                IsAdmin = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            context.AuthUsers.Add(authUser);

            var departmentList = new List<Department>
            {
                new Department
                {
                    Name = "CS",
                    Description = "Computer Science",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CreatedBy = 1,
                    UpdatedBy = 1,
                    Students = null,
                    Teachers = null,
                    Admins = null
                },
                new Department
                {
                    Name = "EEE",
                    Description = "Electrical and Electronics Engineering",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CreatedBy = 1,
                    UpdatedBy = 1,
                    Students = null,
                    Teachers = null,
                    Admins = null
                }
            };

            context.Departments.AddRange(departmentList);

            var studentList = new List<Student>
            {
                new Student
                {
                    FirstName = "Nafisur",
                    LastName = "Rahman",
                    DepartmentId = 1,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    CreatedBy = 1,
                    UpdatedBy = 1,
                    EnrollmentDate = DateTime.Now,
                }
            };

            context.Students.AddRange(studentList);

            //var teacherList = new List<Teacher>
            //{
            //    new Teacher
            //    {
            //        FirstName = "Asheq",
            //        LastName = "Majib",
            //        CreatedAt = DateTime.Now,
            //        UpdatedAt = DateTime.Now,
            //        Salary = 50000,
            //        CreatedBy = 1,
            //        UpdatedBy = 1,
            //        IsInProbation = true,

            //    }
            //};
            base.Seed(context);
        }

    }
}