using DotNetMvcDemo.Data;
using DotNetMvcDemo.Models;
using DotNetMvcDemo.Repository;
using DotNetMvcDemo.UnitOfWork;
using DotNetMvcDemo.ViewModels.Employee.Teacher;
using System;
using System.Collections.Generic;

namespace DotNetMvcDemo.Services
{
    public class TeacherService
    {
        private readonly UnitOfWork<ApplicationDbContext> _unitOfWork = new UnitOfWork<ApplicationDbContext>();
        private readonly IGenericRepository<Teacher> _repository;


        public TeacherService()
        {
            //If you want to use Generic Repository with Unit of work
            _repository = new GenericRepository<Teacher>(_unitOfWork);

        }

        public TeacherService(IGenericRepository<Teacher> repository)
        {
            _repository = repository;
        }

        public IEnumerable<TeacherViewModel> GetTeacherList()
        {

            IEnumerable<Teacher> teachers = _repository.GetAll(null, null, new List<string> { "Department" });
            List<TeacherViewModel> teachersViewList = new List<TeacherViewModel>();

            foreach (Teacher teacher in teachers)
            {
                TeacherViewModel teacherView = new TeacherViewModel();

                teacherView.FirstName = teacher.FirstName;

                teacherView.LastName = teacher.LastName;
                teacherView.TeacherCardNumber = teacher.TeacherCardNumber;
                teacherView.Id = teacher.Id;

                teachersViewList.Add(teacherView);
            }

            return teachersViewList;
        }

        public TeacherDetailsViewModel GetTeacherById(int? id)
        {
            Teacher teacher = _repository.GetById(x => x.Id == id, new List<string> { "Department" });

            if (teacher == null) return null;

            TeacherDetailsViewModel teacherDetailsView = new TeacherDetailsViewModel();

            teacherDetailsView.Id = teacher.Id;
            teacherDetailsView.FirstName = teacher.FirstName;
            teacherDetailsView.LastName = teacher.LastName;
            teacherDetailsView.Departments = teacher.Departments;
            teacherDetailsView.TeacherCardNumber = teacher.TeacherCardNumber;
            teacherDetailsView.IsInProbation = teacher.IsInProbation;
            teacherDetailsView.Salary = teacher.Salary;

            return teacherDetailsView;
        }


        public bool AddNewTeacher(CreateTeacherViewModel viewModel)
        {
            try
            {
                if (viewModel == null) return false;
                Teacher model = new Teacher();
                model.FirstName = viewModel.FirstName;
                model.LastName = viewModel.LastName;
                model.TeacherCardNumber = viewModel.TeacherCardNumber;
                model.CreatedAt = DateTime.Now;
                model.UpdatedAt = DateTime.Now;
                model.CreatedBy = 1;
                model.UpdatedBy = 1;

                _unitOfWork.CreateTransaction();

                _repository.Insert(model);
                _unitOfWork.Save();

                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                //Log the exception and rollback the transaction
                _unitOfWork.Rollback();
                throw;
            }
        }

        public bool UpdateTeacher(TeacherViewModel viewModel)
        {
            if (viewModel == null) return false;
            Teacher model = _repository.GetById(x => x.Id == viewModel.Id);
            if (model == null) return false;
            model.Id = viewModel.Id;
            model.FirstName = viewModel.FirstName;
            model.LastName = viewModel.LastName;
            model.TeacherCardNumber = viewModel.TeacherCardNumber;
            model.IsInProbation = viewModel.IsInProbation;
            model.Salary = viewModel.Salary;
            model.UpdatedAt = DateTime.Now;
            model.UpdatedBy = 1;

            _repository.Update(model);
            _unitOfWork.Save();
            return true;
        }


        public bool DeleteTeacher(int? id)
        {

            Teacher model = _repository.GetById(x => x.Id == id);
            if (model == null) return false;
            _repository.Delete(model);
            _unitOfWork.Save();
            return true;

        }
    }
}