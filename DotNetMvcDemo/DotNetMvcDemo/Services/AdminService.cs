using DotNetMvcDemo.Data;
using DotNetMvcDemo.Models;
using DotNetMvcDemo.Repository;
using DotNetMvcDemo.UnitOfWork;
using DotNetMvcDemo.ViewModels.Employee.Admin;
using System;
using System.Collections.Generic;

namespace DotNetMvcDemo.Services
{
    public class AdminService
    {
        private readonly UnitOfWork<ApplicationDbContext> _unitOfWork = new UnitOfWork<ApplicationDbContext>();
        private readonly IGenericRepository<Admin> _repository;


        public AdminService()
        {
            //If you want to use Generic Repository with Unit of work
            _repository = new GenericRepository<Admin>(_unitOfWork);

        }

        public AdminService(IGenericRepository<Admin> repository)
        {
            _repository = repository;
        }

        public IEnumerable<AdminViewModel> GetAdminList()
        {

            IEnumerable<Admin> admins = _repository.GetAll(null, null, new List<string> { "Department" });
            List<AdminViewModel> adminsViewList = new List<AdminViewModel>();

            foreach (Admin admin in admins)
            {
                AdminViewModel adminView = new AdminViewModel();

                adminView.FirstName = admin.FirstName;

                adminView.LastName = admin.LastName;
                adminView.AdminCardNumber = admin.AdminCardNumber;
                adminView.Id = admin.Id;

                adminsViewList.Add(adminView);
            }

            return adminsViewList;
        }

        public AdminDetailsViewModel GetAdminById(int? id)
        {
            Admin admin = _repository.GetById(x => x.Id == id, new List<string> { "Department" });

            if (admin == null) return null;

            AdminDetailsViewModel adminDetailsView = new AdminDetailsViewModel();

            adminDetailsView.Id = admin.Id;
            adminDetailsView.FirstName = admin.FirstName;
            adminDetailsView.LastName = admin.LastName;

            adminDetailsView.AdminCardNumber = admin.AdminCardNumber;

            adminDetailsView.Salary = admin.Salary;

            return adminDetailsView;
        }


        public bool AddNewAdmin(CreateAdminViewModel viewModel)
        {
            try
            {
                if (viewModel == null) return false;
                Admin model = new Admin();
                model.FirstName = viewModel.FirstName;
                model.LastName = viewModel.LastName;
                model.AdminCardNumber = viewModel.AdminCardNumber;
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

        public bool UpdateAdmin(AdminViewModel viewModel)
        {
            if (viewModel == null) return false;
            Admin model = _repository.GetById(x => x.Id == viewModel.Id);
            if (model == null) return false;
            model.Id = viewModel.Id;
            model.FirstName = viewModel.FirstName;
            model.LastName = viewModel.LastName;
            model.AdminCardNumber = viewModel.AdminCardNumber;

            model.Salary = viewModel.Salary;
            model.UpdatedAt = DateTime.Now;
            model.UpdatedBy = 1;

            _repository.Update(model);
            _unitOfWork.Save();
            return true;
        }


        public bool DeleteAdmin(int? id)
        {

            Admin model = _repository.GetById(x => x.Id == id);
            if (model == null) return false;
            _repository.Delete(model);
            _unitOfWork.Save();
            return true;

        }
    }
}