using DotNetMvcDemo.Models;
using DotNetMvcDemo.Services;
using DotNetMvcDemo.ViewModels.Auth;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace DotNetMvcDemo.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(RegisterViewModel registerView)
        {
            AuthService service = new AuthService();
            Helpers.ServiceResponse response = service.SignupService(registerView);
            if (response == null) return View();
            ViewBag.Message = response.Message;
            ViewBag.Response = response.Response;
            switch (response.Response)
            {
                case Helpers.Response.Success:
                    return RedirectToAction("Login", new { response.Message });
                case Helpers.Response.BadRequest:
                    break;
                case Helpers.Response.Error:
                    return View(registerView);
                case Helpers.Response.Exists:
                    return View(registerView);
                case Helpers.Response.NotFound:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return View();
        }

        public ActionResult Login()
        {
            return HttpContext.User.Identity.IsAuthenticated ? RedirectToAction("Index", "Home") : (ActionResult)View();
        }

        [HttpPost]
        public ActionResult Login(AuthUser authUser)
        {

            AuthService service = new AuthService();
            Helpers.ServiceResponse response = service.LoginService(authUser);
            if (response == null) return View();
            ViewBag.Message = response.Message;
            ViewBag.Response = response.Response;
            switch (response.Response)
            {
                case Helpers.Response.Success:
                    return RedirectToAction("Index", "Home", new { response.Message });
                case Helpers.Response.BadRequest:
                    break;
                case Helpers.Response.Error:
                    return View(authUser);
                case Helpers.Response.Exists:
                    return View(authUser);
                case Helpers.Response.NotFound:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}