using RestApiNetDemo.DAL.Data;
using System;
using System.Collections.Generic;

namespace RestApiNetDemo.Tests.DataAccessLayerTests.TestData
{
    internal class CourseTest
    {
        public List<Cours> GetAll()
        {
            return new List<Cours>()
            {
                new Cours() {Id= 1,Name="Data Structure",Credit=3,CreatedAt=DateTime.Now,UpdatedAt=DateTime.Now,DepartmentId=1},
                new Cours() {Id= 2,Name="Data Structure Lab",Credit=1,CreatedAt=DateTime.Now,UpdatedAt=DateTime.Now,DepartmentId=1},
                new Cours() {Id= 3,Name="Algorithm",Credit=3,CreatedAt=DateTime.Now,UpdatedAt=DateTime.Now,DepartmentId=1},
                new Cours() {Id= 4,Name="Signal Processing",Credit=3,CreatedAt=DateTime.Now,UpdatedAt=DateTime.Now,DepartmentId=2},
            };
        }
    }
}