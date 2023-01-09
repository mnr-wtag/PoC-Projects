using DotNetMvcDemo.Data;
using DotNetMvcDemo.Helpers;
using DotNetMvcDemo.Models;
using DotNetMvcDemo.Repository;
using DotNetMvcDemo.UnitOfWork;
using DotNetMvcDemo.ViewModels.Auth;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Web.Security;

namespace DotNetMvcDemo.Services
{
    public class AuthService
    {
        private readonly UnitOfWork<ApplicationDbContext> _unitOfWork = new UnitOfWork<ApplicationDbContext>();
        private readonly IGenericRepository<AuthUser> _repository;

        public AuthService()
        {
            //If you want to use Generic Repository with Unit of work
            _repository = new GenericRepository<AuthUser>(_unitOfWork);

        }

        public AuthService(IGenericRepository<AuthUser> repository)
        {
            _repository = repository;
        }
        public ServiceResponse SignupService(RegisterViewModel registerView)
        {
            var response = new ServiceResponse();
            var user = new AuthUser
            {
                UserName = registerView.UserName,
                Password = registerView.Password,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            var isUserAlreadyExists = !_repository.GetAll(x => x.UserName == user.UserName).IsNullOrEmpty();
            if (!registerView.Password.Equals(registerView.ConfirmPassword))
            {
                response.Response = Response.Error;
                response.Message = "Password and Confirm Password did not match!";
                return response;
            }
            if (isUserAlreadyExists)
            {
                response.Response = Response.Exists;
                response.Message = "User with same Username already exists! Try with different name.";
                return response;
            }
            _repository.Insert(user);
            _unitOfWork.Save();

            response.Response = Response.Success;
            response.Message = "User sign-up is completed successfully!";
            return response;
        }

        public ServiceResponse LoginService(AuthUser authUser)
        {
            var response = new ServiceResponse();

            var isValidUser = !_repository.GetAll(user => user.UserName.Equals( authUser.UserName, StringComparison.CurrentCultureIgnoreCase) && user.Password == authUser.Password).IsNullOrEmpty();
            if (isValidUser)
            {
                FormsAuthentication.SetAuthCookie(authUser.UserName, false);
                response.Response = Response.Success;
                response.Message = "Authenticated";
                return response;
            }

            response.Response = Response.Error;
            response.Message = "invalid Username or Password";

            return response;

        }
    }
}