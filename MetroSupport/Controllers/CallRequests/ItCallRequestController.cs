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
     [Authorize(Roles = "ItWork, ItBoss, CallCenter, Administrator")]
    public class ItCallRequestController : Controller
    {
        private readonly MetroSupportService _metroSupport;
        public ItCallRequestController(MetroSupportService metroSupport)
        {
            _metroSupport = metroSupport;
        }

        //
        // GET: /ItCallRequest/
        [HttpGet]
        public ActionResult Index(string id = null)
        {
            var callrequest = new ItRequest_LogViewModel(); 
            if (id == null)
            {
                callrequest.Location = new SelectList(_metroSupport.LocationRepository.GetAllLocations(), "LocationName", "LocationName");
                callrequest.User = _metroSupport.UserRepository.GetUserByName(User.Identity.Name);
                return View(callrequest);
            }
            callrequest.ItRequest = _metroSupport.ItCallRepository.GetRequestById(Guid.Parse(id));
            callrequest.ItRequestLog = _metroSupport.ItCallRepository.GetRequestLog(id);
            callrequest.Location = new SelectList(_metroSupport.LocationRepository.GetAllLocations(), "LocationName", "LocationName");
            callrequest.User = _metroSupport.UserRepository.GetUserByName(User.Identity.Name);
            if (callrequest.ItRequest == null)
            {
                return HttpNotFound();
            }
            return View(callrequest);
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAndSave(ItRequest_LogViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                ItCallRequest request;
                if (model.ItRequest.CallRequestId == Guid.Empty)
                {
                    UserProfile assigner = _metroSupport.UserRepository.GetUserByFullName(model.ItRequest.AssignTo);
                    UserProfile boss = _metroSupport.UserRepository.GetUserByFullName(model.ItRequest.AssignBoss);
                    request = _metroSupport.ItCallRepository.SaveCallRequest(model.ItRequest, User.Identity.Name);
                   
                    MailNotificator mailnotify = new MailNotificator();
                    MailData mail = mailnotify.GetMailData(request.CallRequestId, request.RequestNumber, request.TroubleSubject,
                        assigner, boss, request.TroubleDepartment);
                    mailnotify.SendNotificationMessage(mail);
                }
                else
                {
                   request = _metroSupport.ItCallRepository.UpdateCallRequest(model.ItRequest, User.Identity.Name); 
                }
                
                
                return RedirectToAction("Index",new{id=request.CallRequestId.ToString()});
            }
            //model.ItRequest = _metroSupport.ItCallRepository.GetRequestById(model.ItRequest.CallRequestId);
            //model.ItRequestLog = _metroSupport.ItCallRepository.GetRequestLog(model.ItRequest.CallRequestId.ToString());
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
                statement = _metroSupport.ItCallRepository.AcceptToWorkCallRequest(requestid, User.Identity.Name);
            }
            if (statement)
            {
                var callrequest = new ItRequest_LogViewModel();
                callrequest.ItRequest = _metroSupport.ItCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.ItRequest.CallRequestId });
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
            statement = _metroSupport.ItCallRepository.SetOnHoldRequest(requestid,description, User.Identity.Name);
            if (statement)
            {
                var callrequest = new ItRequest_LogViewModel();
                callrequest.ItRequest = _metroSupport.ItCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.ItRequest.CallRequestId });
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
                statement = _metroSupport.ItCallRepository.DenyCallRequest(requestid, description, User.Identity.Name);
            }
            if (statement)
            {
                var callrequest = new ItRequest_LogViewModel();
                callrequest.ItRequest = _metroSupport.ItCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.ItRequest.CallRequestId });
            }
            else
            {
                return HttpNotFound();
            }
        }


        
        public ActionResult SendForAprove(string requestid)
        {

            bool statement = false;
            if (requestid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
          
            statement = _metroSupport.ItCallRepository.SendToAcceptCallRequest(requestid, User.Identity.Name);
            
            if (statement == true)
            {
                var callrequest = new ItRequest_LogViewModel();
                callrequest.ItRequest = _metroSupport.ItCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.ItRequest.CallRequestId });
            }
            else
            {
                return HttpNotFound();
            }
        }
        
       
        public ActionResult CloseRequest(string requestid)
        {
            bool statement = false;
            if (requestid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                statement = _metroSupport.ItCallRepository.CloseCallRequest(requestid, User.Identity.Name);
            }
            if (statement)
            {
                var callrequest = new ItRequest_LogViewModel();
                callrequest.ItRequest = _metroSupport.ItCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.ItRequest.CallRequestId });
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
            var statement = _metroSupport.ItCallRepository.ReturnToAssignerCallRequest(requestid, description, User.Identity.Name);
            if (statement == true)
            {
                var callrequest = new ItRequest_LogViewModel();
                callrequest.ItRequest = _metroSupport.ItCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.ItRequest.CallRequestId });
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
                IQueryable<ItDevice> devices = _metroSupport.DeviceRepository.GetItDevicesByInventoryNumber(invnumber);
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
                IQueryable<ItCategory> basecategories = _metroSupport.ItCategoryRepository.GetAllBaseCategories();
                return Json(basecategories);
            }
            IQueryable<ItCategory> subcategories = _metroSupport.ItCategoryRepository.GetSubCategoriesByCategory(category);
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
        public JsonResult GetDepartments()
        {
           
            IQueryable<Department> departments = _metroSupport.DepartmentRepository.GetDepartmentsByName("It");
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
            IQueryable<Assigner> assigners = _metroSupport.AssignerRepository.GetAssignersByOrganization(department);
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
