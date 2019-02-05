using Myntra.Filters;
using Myntra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Myntra.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        [HttpGet]
        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(SignInModel signInModel, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(signInModel.UserName, signInModel.Password))
                return RedirectToUrl(returnUrl);

            ModelState.AddModelError(string.Empty, "The username or password is not valid!");
            return View(signInModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel registerModel, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(registerModel.UserName, registerModel.Password);
                    WebSecurity.Login(registerModel.UserName, registerModel.Password);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex);
                }
            }

            return View(registerModel);
        }

        #region HELPERS
        public ActionResult RedirectToUrl(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Inbox", "Mailbox");
        }
        #endregion
    }
}
