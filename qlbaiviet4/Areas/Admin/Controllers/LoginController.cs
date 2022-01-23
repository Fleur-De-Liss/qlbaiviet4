using Models.Common;
using Models.DAO;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace qlbaiviet4.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
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
                    userSession.GroupId = user.GroupId;
                    var listCredentials = dao.GetListCredential(model.Username);
                    Session.Add(CommonConstants.SESSION_CREDENTIALS, listCredentials);
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (res == 0)
                {
                    ModelState.AddModelError("", "Invalid Username");
                }
                else if (res == -1)
                {
                    ModelState.AddModelError("", "Invalid Password");
                }
                else if (res == -2)
                {
                    ModelState.AddModelError("", "This account does not have permission");
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