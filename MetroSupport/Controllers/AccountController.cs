using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using MetroSupport.BLL.Services;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using MetroSupport.Filters;
using MetroSupport.Models;

namespace MetroSupport.Controllers
{
    [Authorize]

    public class AccountController : Controller
    {

        private MetroSupportService _metroSupport;
        public AccountController(MetroSupportService metroSupport)
        {
            _metroSupport = metroSupport;
        }
        
        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string ReturnUrl)
        {
            UserProfile user = _metroSupport.UserRepository.GetUserByName(model.UserName);
            if (user != null)
            {
                if (user.Active == true)
                {
                    if (ModelState.IsValid)
                    {
                        if (WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
                        {
                            return RedirectToLocal(ReturnUrl);
                        }
                    }
                }
            }
            ModelState.AddModelError("", "Имя пользователя или пароль указаны неверно.");
            return View(model);

        }


        [Authorize]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Login", "Account");
        }

        [ChildActionOnly]
        public ActionResult UserProfile()
        {
            UserProfile userProfile = _metroSupport.UserRepository.GetUserByName(User.Identity.Name);

            return PartialView("_LoginPartial", userProfile);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
