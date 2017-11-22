using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Hosting;
using System.Web.Security;
using System.Web.Mvc;
using MetroSupport.BLL.Services;
using MetroSupport.Commons;
using WebMatrix.WebData;
using MetroSupport.Models;
using MetroSupport.ViewModels;
using MvcPaging;

namespace MetroSupport.Areas.Administration.Controllers
{
     [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
         private readonly MetroSupportService _metroSupport;
        public UsersController(MetroSupportService metroSupport)
        {
            _metroSupport = metroSupport;
        }
        public ActionResult It(int? page, string literal)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            ViewBag.Literal = literal;
            IPagedList<UserProfile> profiles = String.IsNullOrEmpty(literal) ? _metroSupport.UserRepository.GetUsersByDepartment("It").ToPagedList(currentpage, 10) : _metroSupport.UserRepository.GetUsersByDepartmentAndLiteral("It",literal).ToPagedList(currentpage, 10);
            
            if (profiles == null)
            {
                return HttpNotFound();
            }
            return View(profiles);
        }

        public ActionResult Aspp(int? page, string literal)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            ViewBag.Literal = literal;
            IPagedList<UserProfile> profiles = String.IsNullOrEmpty(literal) ? _metroSupport.UserRepository.GetUsersByDepartment("Aspp").ToPagedList(currentpage, 10) : _metroSupport.UserRepository.GetUsersByDepartmentAndLiteral("Aspp", literal).ToPagedList(currentpage, 10);

            if (profiles == null)
            {
                return HttpNotFound();
            }
            return View(profiles);
        }

        public ActionResult Svyaz(int? page, string literal)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            ViewBag.Literal = literal;
            IPagedList<UserProfile> profiles = String.IsNullOrEmpty(literal) ? _metroSupport.UserRepository.GetUsersByDepartment("Svyaz").ToPagedList(currentpage, 10) : _metroSupport.UserRepository.GetUsersByDepartmentAndLiteral("Svyaz", literal).ToPagedList(currentpage, 10);

            if (profiles == null)
            {
                return HttpNotFound();
            }
            return View(profiles);
        }

        public ActionResult Pa(int? page, string literal)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            ViewBag.Literal = literal;
            IPagedList<UserProfile> profiles = String.IsNullOrEmpty(literal) ? _metroSupport.UserRepository.GetUsersByDepartment("Pa").ToPagedList(currentpage, 10) : _metroSupport.UserRepository.GetUsersByDepartmentAndLiteral("Pa", literal).ToPagedList(currentpage, 10);

            if (profiles == null)
            {
                return HttpNotFound();
            }
            return View(profiles);
        }

        
        public ActionResult Registration()
        {
            RegisterModel model = new RegisterModel();
            List<RoleViewModel> roleslist = new List<RoleViewModel>();
            string[] allroles = Roles.GetAllRoles();
            for (var i=0; i< Roles.GetAllRoles().Count(); i++)
            {
                RoleViewModel role = new RoleViewModel
                {
                    RoleName = allroles[i],
                    Selected = false
                };
                roleslist.Add(role);
            }
            model.Roles = roleslist;
 
            return View(model);
        }


        public ActionResult Manage(string username)
        {

            List<RoleViewModel> roleslist = new List<RoleViewModel>();
            string[] allroles = Roles.GetAllRoles();
            for (var i = 0; i < allroles.Count(); i++)
            {
                RoleViewModel role = new RoleViewModel();
                role.RoleName = allroles[i];
                if (Roles.IsUserInRole(username, role.RoleName))
                {
                    role.Selected = true;
                }
                roleslist.Add(role);
            }
            ManageModel profile = _metroSupport.UserRepository.GetUserByName(username).ToManageModel();
            profile.Roles = roleslist;

            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(ManageModel model)
        {
            if (Roles.GetRolesForUser(model.UserName).Any())
            {
                Roles.RemoveUserFromRoles(model.UserName, Roles.GetRolesForUser(model.UserName));
            }
            foreach (var role in model.Roles)
            {
                if (role.Selected)
                {
                    Roles.AddUserToRole(model.UserName, role.RoleName);
                }

            }

            List<RoleViewModel> roleslist = new List<RoleViewModel>();
            string[] allroles = Roles.GetAllRoles();
            for (var i = 0; i < allroles.Count(); i++)
            {
                RoleViewModel role = new RoleViewModel();
                role.RoleName = allroles[i];
                if (Roles.IsUserInRole(model.UserName, role.RoleName))
                {
                    role.Selected = true;
                }
                roleslist.Add(role);
            }

            ManageModel manage = _metroSupport.UserRepository.SaveUserProfile(model.ToUserProfile()).ToManageModel();
            manage.Roles = roleslist;

            return View(manage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Попытка зарегистрировать пользователя
                try
                {
                  if (!WebSecurity.UserExists(model.UserName))
                  {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new { FullName = model.FullName, Job = model.Job, Slugba = model.Slugba, Department = model.Department, Email = model.Email, Active = true });

                    
                        foreach (var role in model.Roles)
                        {
                            if (role.Selected)
                            {
                                Roles.AddUserToRole(model.UserName, role.RoleName);
                            }

                        }
                  }

                    return RedirectToAction("It", "Users");

                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
          
            // Появление этого сообщения означает наличие ошибки; повторное отображение формы
            return View(model);
        }

        public ActionResult DeleteUser(string userName)
        {


            if (Roles.GetRolesForUser(userName).Any())
            {
                Roles.RemoveUserFromRoles(userName, Roles.GetRolesForUser(userName));
            }
            ((SimpleMembershipProvider) Membership.Provider).DeleteAccount(userName);
            ((SimpleMembershipProvider) Membership.Provider).DeleteUser(userName, true);

            return Redirect(HttpContext.Request.UrlReferrer.PathAndQuery); 
        }


        [HttpPost]
        public JsonResult ChangePassword(string username, string newpassword)
        {
            if (!String.IsNullOrWhiteSpace(username) && !String.IsNullOrWhiteSpace(newpassword))
            {
                bool changePasswordSucceeded;
                try
                {
                    string resettoken = WebSecurity.GeneratePasswordResetToken(username);
                    changePasswordSucceeded = WebSecurity.ResetPassword(resettoken, newpassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return Json(true);
                }
                return Json(false);
            }
            return Json(false);
        }

        [ChildActionOnly]
        public ActionResult UserProfile()
        {
            UserProfile userProfile = _metroSupport.UserRepository.GetUserByName(User.Identity.Name);

            return PartialView("_LoginPartial", userProfile);
        }

        #region Вспомогательные методы
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

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }


        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // Полный список кодов состояния см. по адресу http://go.microsoft.com/fwlink/?LinkID=177550
            //.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Имя пользователя уже существует. Введите другое имя пользователя.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Имя пользователя для данного адреса электронной почты уже существует. Введите другой адрес электронной почты.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Указан недопустимый пароль. Введите допустимое значение пароля.";

                case MembershipCreateStatus.InvalidEmail:
                    return "Указан недопустимый адрес электронной почты. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "Указан недопустимый ответ на вопрос для восстановления пароля. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "Указан недопустимый вопрос для восстановления пароля. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Указано недопустимое имя пользователя. Проверьте значение и повторите попытку.";

                case MembershipCreateStatus.ProviderError:
                    return "Поставщик проверки подлинности вернул ошибку. Проверьте введенное значение и повторите попытку. Если проблему устранить не удастся, обратитесь к системному администратору.";

                case MembershipCreateStatus.UserRejected:
                    return "Запрос создания пользователя был отменен. Проверьте введенное значение и повторите попытку. Если проблему устранить не удастся, обратитесь к системному администратору.";

                default:
                    return "Произошла неизвестная ошибка. Проверьте введенное значение и повторите попытку. Если проблему устранить не удастся, обратитесь к системному администратору.";
            }
        }
        #endregion

    }
}
