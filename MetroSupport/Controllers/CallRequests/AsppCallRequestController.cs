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
     [Authorize(Roles = "AsppWork, AsppBoss, CallCenter, Administrator")]
    public class AsppCallRequestController : Controller
    {


        private readonly MetroSupportService _metroSupport;
        public AsppCallRequestController(MetroSupportService metroSupport)
        {
            _metroSupport = metroSupport;
        }

        //
        // GET: /ItCallRequest/
        [HttpGet]
        public ActionResult Index(string id = null)
        {
            var callrequest = new AsppRequest_LogViewModel();
            if (id == null)
            {
                callrequest.Location = new SelectList(_metroSupport.LocationRepository.GetAllLocations(), "LocationName", "LocationName");
                callrequest.User = _metroSupport.UserRepository.GetUserByName(User.Identity.Name);
                return View(callrequest);
            }
            callrequest.AsppRequest = _metroSupport.AsppCallRepository.GetRequestById(Guid.Parse(id));
            callrequest.AsppRequestLog = _metroSupport.AsppCallRepository.GetRequestLog(id);
            callrequest.Location = new SelectList(_metroSupport.LocationRepository.GetAllLocations(), "LocationName", "LocationName");
            callrequest.User = _metroSupport.UserRepository.GetUserByName(User.Identity.Name);
            if (callrequest.AsppRequest.CallRequestId.ToString() == String.Empty)
            {
                return HttpNotFound();
            }
            return View(callrequest);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAndSave(AsppRequest_LogViewModel model)
        {
            if (ModelState.IsValid)
            {
                AsppCallRequest request;
                if (model.AsppRequest.CallRequestId == Guid.Empty)
                {
                    UserProfile assigner = _metroSupport.UserRepository.GetUserByFullName(model.AsppRequest.AssignTo);
                    UserProfile boss = _metroSupport.UserRepository.GetUserByFullName(model.AsppRequest.AssignBoss);
                    request = _metroSupport.AsppCallRepository.SaveCallRequest(model.AsppRequest, User.Identity.Name);
                    MailNotificator mailnotify = new MailNotificator();
                    MailData mail = mailnotify.GetMailData(request.CallRequestId, request.RequestNumber, request.TroubleSubject,
                        assigner, boss, request.TroubleDepartment);
                    mailnotify.SendNotificationMessage(mail);
                }
                else
                {
                    request = _metroSupport.AsppCallRepository.UpdateCallRequest(model.AsppRequest, User.Identity.Name);
                }
                if (request == null)
                {
                    return HttpNotFound();
                }

                return RedirectToAction("Index", new { id = request.CallRequestId.ToString() });
            }
            //model.AsppRequest = _metroSupport.AsppCallRepository.GetRequestById(model.AsppRequest.CallRequestId);
            //model.AsppRequestLog = _metroSupport.AsppCallRepository.GetRequestLog(model.AsppRequest.CallRequestId.ToString());
            model.Location = new SelectList(_metroSupport.LocationRepository.GetAllLocations(), "LocationName", "LocationName");
            model.User = _metroSupport.UserRepository.GetUserByName(User.Identity.Name);
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Accept(string requestid)
        {
            bool statement = false;
            if (requestid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                statement = _metroSupport.AsppCallRepository.AcceptToWorkCallRequest(requestid, User.Identity.Name);
            }
            if (statement)
            {
                var callrequest = new AsppRequest_LogViewModel();
                callrequest.AsppRequest = _metroSupport.AsppCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.AsppRequest.CallRequestId });
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult MakeHold(string requestid, string description)
        {
            bool statement = false;
            if (requestid == null || description == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            statement = _metroSupport.AsppCallRepository.SetOnHoldRequest(requestid, description, User.Identity.Name);
            if (statement)
            {
                var callrequest = new AsppRequest_LogViewModel();
                callrequest.AsppRequest = _metroSupport.AsppCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.AsppRequest.CallRequestId });
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
                statement = _metroSupport.AsppCallRepository.DenyCallRequest(requestid, description, User.Identity.Name);
            }
            if (statement == true)
            {
                var callrequest = new AsppRequest_LogViewModel();
                callrequest.AsppRequest = _metroSupport.AsppCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.AsppRequest.CallRequestId });
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
                statement = _metroSupport.AsppCallRepository.SendToAcceptCallRequest(requestid, User.Identity.Name);
            }
            if (statement == true)
            {
                var callrequest = new AsppRequest_LogViewModel();
                callrequest.AsppRequest = _metroSupport.AsppCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.AsppRequest.CallRequestId });
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
                statement = _metroSupport.AsppCallRepository.CloseCallRequest(requestid, User.Identity.Name);
            }
            if (statement == true)
            {
                var callrequest = new AsppRequest_LogViewModel();
                callrequest.AsppRequest = _metroSupport.AsppCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.AsppRequest.CallRequestId });
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
            var statement = _metroSupport.AsppCallRepository.ReturnToAssignerCallRequest(requestid, description, User.Identity.Name);
            if (statement == true)
            {
                var callrequest = new AsppRequest_LogViewModel();
                callrequest.AsppRequest = _metroSupport.AsppCallRepository.GetRequestById(Guid.Parse(requestid));

                return RedirectToAction("Index", new { id = callrequest.AsppRequest.CallRequestId });
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
                IQueryable<AsppDevice> devices = _metroSupport.DeviceRepository.GetAsppDevicesByInventoryNumber(invnumber);
                if (devices != null)
                {
                    return Json(devices);

                }

            }
            return Json(null);
        }


        [HttpPost]
        public JsonResult GetCategories(string category)
        {
            if (String.IsNullOrEmpty(category))
            {
                IQueryable<AsppCategory> basecategories = _metroSupport.AsppCategoryRepository.GetAllBaseCategories();
                return Json(basecategories);
            }
            IQueryable<AsppCategory> subcategories = _metroSupport.AsppCategoryRepository.GetSubCategoriesByCategory(category);
            if (subcategories == null)
            {
                return Json(null);

            }
            return Json(subcategories);
        }

        [HttpPost]
        public JsonResult GetRequestors(string requestor)
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
        public JsonResult GetThemes(string department = "Aspp")
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
        public JsonResult GetDepartments(string organization = "Aspp")
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
