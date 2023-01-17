using DotNetMvcDemo.Data;
using DotNetMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DotNetMvcDemo.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            AuthUser authUser = new AuthUser
            {
                UserName = "admin",
                Password = "admin",
                IsAdmin = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            context.AuthUsers.AddOrUpdate(authUser);
            context.SaveChanges();

            List<Department> departmentList = new List<Department>
            {
                new Department
                {
                    Name = "CS", Description = "Computer Science", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Students = null, Teachers = null, Admins = null,CreatedBy=1,UpdatedBy=1},
                new Department
                {Name = "EEE", Description = "Electrical and Electronics Engineering", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, Students = null, Teachers = null, Admins = null,CreatedBy=1,UpdatedBy=1}
            };

            foreach (Department department in departmentList)
            {
                context.Departments.AddOrUpdate(department);
                context.SaveChanges();
            }


            //List<Student> studentList = new List<Student>
            //{
            //    new Student
            //    {
            //        FirstName = "Nafisur",
            //        LastName = "Rahman",
            //        DepartmentId = 1,
            //        CreatedAt = DateTime.Now,
            //        UpdatedAt = DateTime.Now,
            //        CreatedBy=1,
            //        UpdatedBy=1,
            //        EnrollmentDate = DateTime.Now,
            //    }
            //};
            //foreach (Student student in studentList)
            //{
            //    context.Students.AddOrUpdate(student);
            //    context.SaveChanges();
            //}


            //List<Course> courseList = new List<Course>()
            //{
            //    new Course
            //    {
            //        Name = "Data Structure",
            //        Credit = 3,
            //        DepartmentId = 1,
            //        CreatedAt = DateTime.Now,
            //        UpdatedAt = DateTime.Now,
            //        CreatedBy=1,UpdatedBy=1
            //    },
            //    new Course
            //    {
            //        Name = "Algorithm",
            //        Credit = 3,
            //        DepartmentId = 1,
            //        CreatedAt = DateTime.Now,
            //        UpdatedAt = DateTime.Now,
            //        CreatedBy=1,UpdatedBy=1
            //    },
            //    new Course
            //    {
            //        Name = "Introduction to Programming",
            //        Credit = 1,
            //        DepartmentId = 1,
            //        CreatedAt = DateTime.Now,
            //        UpdatedAt = DateTime.Now,
            //        CreatedBy=1,UpdatedBy=1
            //    },
            //    new Course
            //    {
            //        Name = "Electric Circuits DC",
            //        Credit = 3,
            //        DepartmentId = 2,
            //        CreatedAt = DateTime.Now,
            //        UpdatedAt = DateTime.Now,
            //        CreatedBy=1,UpdatedBy=1
            //    },
            //    new Course
            //    {
            //        Name = "Electric Circuits DC - Lab",
            //        Credit = 1,
            //        DepartmentId = 2,
            //        CreatedAt = DateTime.Now,
            //        UpdatedAt = DateTime.Now,
            //        CreatedBy=1,UpdatedBy=1
            //    },
            //};
            //foreach (Course course in courseList)
            //{
            //    context.Courses.AddOrUpdate(course);
            //    context.SaveChanges();
            //}

            base.Seed(context);
        }
    }
}
