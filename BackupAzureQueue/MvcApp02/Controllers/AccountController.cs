using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp02.Models;
using WebMatrix.WebData;

namespace MvcApp02.Controllers
{
    public class AccountController : Controller
    {
        private RestaurantDb db = new RestaurantDb();

        //
        // GET: /Account/

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Logon()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Logon(LogonViewModel logonViewModel)
        {
            if (ModelState.IsValid && WebSecurity.Login(logonViewModel.UserName, logonViewModel.Password, false))
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("UserNotAuthenticated", "User is not identified");
            return View(logonViewModel);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Register()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                WebSecurity.CreateUserAndAccount(registerViewModel.UserName, registerViewModel.Password, null, false);
                WebSecurity.Login(registerViewModel.UserName, registerViewModel.Password, false);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("UserNotAuthenticated", "User is not identified");
            return View(registerViewModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Logout()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}