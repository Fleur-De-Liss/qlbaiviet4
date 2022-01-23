using Models.Common;
using Models.DAO;
using Models.EF;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace qlbaiviet4.Controllers
{
    public class MemberController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                if (dao.checkUsername(model.Username))
                {
                    ModelState.AddModelError("", "Username already exists");
                }
                else if (dao.checkEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email already exists");
                }
                else
                {
                    var user = new User();
                    user.Username = model.Username;
                    user.Email = model.Email;
                    user.Password = model.Password;
                    user.GroupId = model.GroupId;
                    var res = dao.Insert(user);
                    if (res > 0)
                    {
                        ViewBag.Success = "Register sucessfully";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Register fail");
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var res = dao.Login(model.Username, Encryptor.MD5Hash(model.Password));
                if (res == 1)
                {
                    var user = dao.GetByUsername(model.Username);
                    var userSession = new UserSession();
                    userSession.Username = user.Username;
                    userSession.UserId = user.UserId;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("/");
                }
                else if (res == 0)
                {
                    ModelState.AddModelError("", "Invalid Username");
                }
                else if (res == -1)
                {
                    ModelState.AddModelError("", "Invalid Password");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password");
                }
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}