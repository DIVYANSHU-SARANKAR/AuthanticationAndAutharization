using DemoMVCApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DemoMVCApp.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult RegisterStudentInfo()
        {
            return View("RegistrationView");
        }
        [HttpPost]
        public ActionResult RegisterStudentInfo(LoginModel loginModel)
        {
            LoginModelManager OLoginModelManager = new LoginModelManager();
            OLoginModelManager.InsertStudentInfo(loginModel);
            RedirectToRouteResult rr = RedirectToAction("Login", "Login");
            ActionResult ar = rr;
            return ar;

        }
        [HttpGet]
        public ActionResult login()
        {
            LoginModel OLoginModel = new LoginModel();
            return View(OLoginModel);
        }
        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            LoginModelManager OLoginModelManager = new LoginModelManager();
            loginModel = OLoginModelManager.UserAuthentication(loginModel);
            if (loginModel.IsValid==1)
            {
                Session["StudentEmail"] = loginModel.UserEmail;
                FormsAuthentication.SetAuthCookie(loginModel.UserName, false);

                var authTicket = new FormsAuthenticationTicket(1, loginModel.UserEmail, DateTime.Now, DateTime.Now.AddMinutes(20), false, loginModel.Role);

                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                HttpContext.Response.Cookies.Add(authCookie);
                return RedirectToAction("Index", "Home", loginModel);

            
            }
            else
            {
                loginModel.LoginErrorMessage = "Wrong UserName Or Password";
                return View("Login", loginModel);
            }
        }
    }
}