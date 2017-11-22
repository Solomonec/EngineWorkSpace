using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MetroSupport.BLL.Implements;
using MetroSupport.BLL.Services;
using MetroSupport.Models;
using MetroSupport.ViewModels;

namespace MetroSupport.Controllers.CallRequests
{
     [Authorize(Roles = "SvyazWork, SvyazBoss, CallCenter, Administrator")]
    public class SvyazCallRequestController : Controller
    {
        //
       private readonly MetroSupportService _metroSupport;
        public SvyazCallRequestController(MetroSupportService metroSupport)
        {
            _metroSupport = metroSupport;
        }

        
        [HttpGet]
        public ActionResult Index(string id = null)
        {
            var callrequest = new SvyazRequest_LogViewModel(); 
            if (id == null)
            {
                callrequest.Location = new SelectList(_metroSupport.LocationRepository.GetAllLocations(), "LocationName", "LocationName");
                callrequest.User = _metroSupport.UserRepository.GetUserByName(User.Identity.Name);
                return View(callrequest);
            }
            callrequest.SvyazRequest = _metroSupport.SvyazCallRepository.GetRequestById(Guid.Parse(id));
            callrequest.SvyazRequestLog = _metroSupport.SvyazCallRepository.GetRequestLog(id);
            callrequest.Location = new SelectList(_metroSupport.LocationRepository.GetAllLocations(), "LocationName", "LocationName");
            callrequest.User = _metroSupport.UserRepository.GetUserByName(User.Identity.Name);
            if (callrequest.SvyazRequest.CallRequestId.ToString() == String.Empty)
            {
                return HttpNotFound();
            }
            return View(callrequest);
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAndSave(SvyazRequest_LogViewModel model)
        {
            if (ModelState.IsValid)
            {
                SvyazCallRequest request;
                if (model.SvyazRequest.CallRequestId == Guid.Empty)
                {
                    UserProfile assigner = _metroSupport.UserRepository.GetUserByFullName(model.SvyazRequest.AssignTo);
                    UserProfile boss = _metroSupport.UserRepository.GetUserByFullName(model.SvyazRequest.AssignBoss);
                    request = _metroSupport.SvyazCallRepository.SaveCallRequest(model.SvyazRequest, User.Identity.Name);

                    MailNotificator mailnotify = new MailNotificator();
                    MailData mail = mailnotify.GetMailData(request.CallRequestId, request.RequestNumber, request.TroubleSubject,
                        assigner, boss, request.TroubleDepartment);
                    mailnotify.SendNotificationMessage(mail);
                }
                else
                {
                    request = _metroSupport.SvyazCallRepository.UpdateCallRequest(model.SvyazRequest, User.Identity.Name);
                }

                if (request == null)
                {
                    return HttpNotFound();
                }

                return RedirectToAction("Index", new { id = request.ToString() });

               
            }
            model.Location = new SelectList(_metroSupport.LocationRepository.GetAllLocations(), "LocationName", "LocationName");
            model.User = _metroSupport.UserRepository.GetUserByName(User.Identity.Name);
            return View("Index",model);
        }

      
        public ActionResult Accept(string requestid)
        {
            bool statement = false;
            if(requestid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                statement = _metroSupport.SvyazCallRepository.AcceptToWorkCallRequest(requestid, User.Identity.Name);
            }
            if (statement)
            {
                var callrequest = new SvyazRequest_LogViewModel();
                callrequest.SvyazRequest = _metroSupport.SvyazCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.SvyazRequest.CallRequestId });
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult MakeHold(string requestid, string description)
        {
            bool statement = false;
            if(requestid == null || description == null) 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            statement = _metroSupport.SvyazCallRepository.SetOnHoldRequest(requestid, description, User.Identity.Name);
            if (statement)
            {
                var callrequest = new SvyazRequest_LogViewModel();
                callrequest.SvyazRequest = _metroSupport.SvyazCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.SvyazRequest.CallRequestId });
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Deny(string requestid, string description)
        {

            bool statement = false;
            if (requestid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                statement = _metroSupport.SvyazCallRepository.DenyCallRequest(requestid, description, User.Identity.Name);
            }
            if (statement)
            {
                var callrequest = new SvyazRequest_LogViewModel();
                callrequest.SvyazRequest = _metroSupport.SvyazCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.SvyazRequest.CallRequestId });
            }
            else
            {
                return HttpNotFound();
            }
        }


        [HttpPost]
        public ActionResult SendForAprove(string requestid)
        {

            bool statement = false;
            if (requestid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                statement = _metroSupport.SvyazCallRepository.SendToAcceptCallRequest(requestid, User.Identity.Name);
            }
            if (statement)
            {
                var callrequest = new SvyazRequest_LogViewModel();
                callrequest.SvyazRequest = _metroSupport.SvyazCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.SvyazRequest.CallRequestId });
            }
            else
            {
                return HttpNotFound();
            }
        }
        
        [HttpPost]
        public ActionResult CloseRequest(string requestid)
        {
            bool statement = false;
            if (requestid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                statement = _metroSupport.SvyazCallRepository.CloseCallRequest(requestid, User.Identity.Name);
            }
            if (statement == true)
            {
                var callrequest = new SvyazRequest_LogViewModel();
                callrequest.SvyazRequest = _metroSupport.SvyazCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.SvyazRequest.CallRequestId });
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult ReturnToAssigner(string requestid, string description)
        {
            if (requestid == null || description == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var statement = _metroSupport.SvyazCallRepository.ReturnToAssignerCallRequest(requestid, description, User.Identity.Name);
            if (statement == true)
            {
                var callrequest = new SvyazRequest_LogViewModel();
                callrequest.SvyazRequest = _metroSupport.SvyazCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.SvyazRequest.CallRequestId });
            }
            else
            {
                return HttpNotFound();
            }
        }


        [HttpPost]
        public JsonResult GetCategories (string category)
        {
            if (String.IsNullOrEmpty(category))
            {
                IQueryable<SvyazCategory> basecategories = _metroSupport.SvyazCategoryRepository.GetAllBaseCategories();
                return Json(basecategories);
            }
            IQueryable<SvyazCategory> subcategories = _metroSupport.SvyazCategoryRepository.GetSubCategoriesByCategory(category);
            if (subcategories == null)
            {
                return Json(null);

            }
            return Json(subcategories);
        }

        [HttpPost]
        public JsonResult GetRequestors (string requestor)
        {
            if (String.IsNullOrWhiteSpace(requestor))
            {
                return Json(null);
            }
            IQueryable<RequestOwner> requestors = _metroSupport.RequestOwnerRepository.GetRequestOwnersByName(requestor);
            if (requestors == null)
            {
                return Json(null);
                
            }
            return Json(requestors);
        }

        [HttpPost]
        public JsonResult GetThemes(string department)
        {
            if (String.IsNullOrWhiteSpace(department))
            {
                return Json(null);
            }
            IQueryable<TroubleSubject> themes = _metroSupport.TroubleSubjectRepository.GetTroubleSubjectsByDepartment(department);
            if (themes == null)
            {
                return Json(null);

            }
            return Json(themes);
        }

        [HttpPost]
        public JsonResult GetDepartments(string organization = "Svyaz")
        {
            if (String.IsNullOrWhiteSpace(organization))
            {
                return Json(null);
            }
            IQueryable<Department> departments = _metroSupport.DepartmentRepository.GetDepartmentsByName(organization);
            if (departments == null)
            {
                return Json(null);

            }
            return Json(departments);
        }

        [HttpPost]
        public JsonResult GetAssigners(string department)
        {
            if (String.IsNullOrWhiteSpace(department))
            {
                return Json(null);
            }
            IQueryable<Assigner> assigners = _metroSupport.AssignerRepository.GetAssignersByDepartment(department);
            if (assigners == null)
            {
                return Json(null);

            }
            return Json(assigners);
        }


    }
}
