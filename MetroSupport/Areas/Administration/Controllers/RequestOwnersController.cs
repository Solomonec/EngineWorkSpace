using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MetroSupport.Areas.Administration.ViewModels;
using MetroSupport.BLL.Services;
using MetroSupport.Models;
using MvcPaging;

namespace MetroSupport.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RequestOwnersController : Controller
    {
        
        private readonly MetroSupportService _metroSupport;
        public RequestOwnersController(MetroSupportService metroSupport)
        {
            _metroSupport = metroSupport;
        }

        [HttpGet]
        public ActionResult Index(int? page, string literal)
        {
            int currentpage = (int) (page.HasValue ? page - 1 : 0);
            RequestOwnerViewModel requestors = new RequestOwnerViewModel();
            if(literal == null)
            requestors.RequestOwners = _metroSupport.RequestOwnerRepository.GetRequestOwners().ToPagedList(currentpage, 25);
            else
            {
                ViewBag.Literal = literal;
                requestors.RequestOwners = _metroSupport.RequestOwnerRepository.GetRequestOwnersByLiteral(literal).ToPagedList(currentpage, 25);
            }
            if (requestors.RequestOwners == null)
            {
                return HttpNotFound();
            }
            return View(requestors);
        }

        [HttpPost]
        public JsonResult RequestOwnerInfo(string requestorid)
        {
            if (requestorid == null)
            {
                return Json(false);
            }
            var requestor = _metroSupport.RequestOwnerRepository.GetRequestOwnerById(requestorid);
            if (requestor == null)
            {
                return Json(false);
            }
            return Json(requestor);
        }

        
        public ActionResult RemoveRequestOwner(string requestorid)
        {
            if (requestorid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool statusresult = _metroSupport.RequestOwnerRepository.DeleteOwner(requestorid);
            if (!statusresult)
            {
                return HttpNotFound();
            }

            return Redirect(HttpContext.Request.UrlReferrer.PathAndQuery); 
        }

        [HttpPost]
        public JsonResult CreateRequestOwner(RequestOwner requestor)
        {
            bool statment = false;
            if (ModelState.IsValid)
            {
                statment = requestor.RequestorId == Guid.Empty ? _metroSupport.RequestOwnerRepository.CreateNewOwner(requestor) : _metroSupport.RequestOwnerRepository.SaveOwnerChanges(requestor);

            }
            return Json(statment);
        }

      
    }
}
