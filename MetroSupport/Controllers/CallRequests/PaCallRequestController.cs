using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DeviceBase.Models;
using MetroSupport.BLL.Implements;
using MetroSupport.BLL.Services;
using MetroSupport.Models;
using MetroSupport.ViewModels;

namespace MetroSupport.Controllers
{
     [Authorize(Roles = "PaWork, PaBoss, CallCenter, Administrator")]
    public class PaCallRequestController : Controller
    {
       private readonly MetroSupportService _metroSupport;
        public PaCallRequestController(MetroSupportService metroSupport)
        {
            _metroSupport = metroSupport;
        }

       
        [HttpGet]
        public ActionResult Index(string id = null)
        {
            var callrequest = new PaRequest_LogViewModel(); 
            if (id == null)
            {
                callrequest.Location = new SelectList(_metroSupport.LocationRepository.GetAllLocations(), "LocationName", "LocationName");
                callrequest.User = _metroSupport.UserRepository.GetUserByName(User.Identity.Name);
                return View(callrequest);
            }
            callrequest.PaRequest = _metroSupport.PaCallRepository.GetRequestById(Guid.Parse(id));
            callrequest.PaRequestLog = _metroSupport.PaCallRepository.GetRequestLog(id);
            callrequest.Location = new SelectList(_metroSupport.LocationRepository.GetAllLocations(), "LocationName", "LocationName");
            callrequest.User = _metroSupport.UserRepository.GetUserByName(User.Identity.Name);
            if (callrequest.PaRequest.CallRequestId.ToString() == String.Empty)
            {
                return HttpNotFound();
            }
            return View(callrequest);
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAndSave(PaRequest_LogViewModel model)
        {
            if (ModelState.IsValid)
            {
                PaCallRequest request;
                if (model.PaRequest.CallRequestId == Guid.Empty)
                {
                    UserProfile assigner = _metroSupport.UserRepository.GetUserByFullName(model.PaRequest.AssignTo);
                    UserProfile boss = _metroSupport.UserRepository.GetUserByFullName(model.PaRequest.AssignBoss);
                    request = _metroSupport.PaCallRepository.SaveCallRequest(model.PaRequest, User.Identity.Name);
                    
                    MailNotificator mailnotify = new MailNotificator();
                    MailData mail = mailnotify.GetMailData(request.CallRequestId, request.RequestNumber, request.TroubleSubject,
                        assigner, boss, request.TroubleDepartment);
                    mailnotify.SendNotificationMessage(mail);
                }
                else
                {
                    request = _metroSupport.PaCallRepository.UpdateCallRequest(model.PaRequest, User.Identity.Name);
                }
                if (request == null)
                {
                    return HttpNotFound();
                }
                return RedirectToAction("Index", new { id = request.CallRequestId.ToString() });
            }
            //model.PaRequest = _metroSupport.PaCallRepository.GetRequestById(model.PaRequest.CallRequestId);
            //model.PaRequestLog = _metroSupport.PaCallRepository.GetRequestLog(model.PaRequest.CallRequestId.ToString());
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
                statement = _metroSupport.PaCallRepository.AcceptToWorkCallRequest(requestid, User.Identity.Name);
            }
            if (statement)
            {
                var callrequest = new PaRequest_LogViewModel();
                callrequest.PaRequest = _metroSupport.PaCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.PaRequest.CallRequestId });
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
            statement = _metroSupport.PaCallRepository.SetOnHoldRequest(requestid,description, User.Identity.Name);
            if (statement)
            {
                var callrequest = new PaRequest_LogViewModel();
                callrequest.PaRequest = _metroSupport.PaCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.PaRequest.CallRequestId });
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
                statement = _metroSupport.PaCallRepository.DenyCallRequest(requestid,description, User.Identity.Name);
            }
            if (statement)
            {
                var callrequest = new PaRequest_LogViewModel();
                callrequest.PaRequest = _metroSupport.PaCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.PaRequest.CallRequestId });
            }
            else
            {
                return HttpNotFound();
            }
        }


        [HttpGet]
        public ActionResult SendForAprove(string requestid)
        {

            bool statement = false;
            if (requestid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                statement = _metroSupport.PaCallRepository.SendToAcceptCallRequest(requestid, User.Identity.Name);
            }
            if (statement)
            {
                var callrequest = new PaRequest_LogViewModel();
                callrequest.PaRequest = _metroSupport.PaCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.PaRequest.CallRequestId });
            }
            else
            {
                return HttpNotFound();
            }
        }
        
        [HttpGet]
        public ActionResult CloseRequest(string requestid)
        {
            bool statement = false;
            if (requestid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                statement = _metroSupport.PaCallRepository.CloseCallRequest(requestid, User.Identity.Name);
            }
            if (statement == true)
            {
                var callrequest = new PaRequest_LogViewModel();
                callrequest.PaRequest = _metroSupport.PaCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.PaRequest.CallRequestId });
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
            var statement = _metroSupport.PaCallRepository.ReturnToAssignerCallRequest(requestid, description, User.Identity.Name);
            if (statement == true)
            {
                var request = new PaRequest_LogViewModel();
                request.PaRequest = _metroSupport.PaCallRepository.GetRequestById(Guid.Parse(requestid));
                request.PaRequestLog = _metroSupport.PaCallRepository.GetRequestLog(requestid);
                return View("Index", request);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public JsonResult GetDevices(string invnumber)
        {
            if (!String.IsNullOrEmpty(invnumber))
            {
                IQueryable<PaDevice> devices = _metroSupport.DeviceRepository.GetPaDevicesByInventoryNumber(invnumber);
                if (devices != null)
                {
                    return Json(devices);

                }

            }
            return Json(null);
        }

        [HttpPost]
        public JsonResult GetCategories (string category)
        {
            if (String.IsNullOrEmpty(category))
            {
                IQueryable<PaCategory> basecategories = _metroSupport.PaCategoryRepository.GetAllBaseCategories();
                return Json(basecategories);
            }
            IQueryable<PaCategory> subcategories = _metroSupport.PaCategoryRepository.GetSubCategoriesByCategory(category);
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
        public JsonResult GetDepartments(string organization = "Pa")
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

        [HttpPost]
        public JsonResult GetDeviceModels(string index)
        {
            if (String.IsNullOrWhiteSpace(index))
            {
                return Json(null);
            }
            IQueryable<DeviceModel> devicemodels  = _metroSupport.DeviceModelRepository.GetDevicesModelsByIndexator(index);
            if (devicemodels == null)
            {
                return Json(null);
                
            }
            return Json(devicemodels);
        }
    }
}
