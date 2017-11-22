using System;
using System.Net;
using System.Web.Mvc;
using MetroSupport.Areas.Administration.ViewModels;
using MetroSupport.BLL.Services;
using MetroSupport.Models;
using MvcPaging;

namespace MetroSupport.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LocationsController : Controller
    {
        private readonly MetroSupportService _metroSupport;
        public LocationsController(MetroSupportService metroSupport)
        {
            _metroSupport = metroSupport;
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            int currentpage = (int) (page.HasValue ? page - 1 : 0);
            LocationViewModel locations = new LocationViewModel();
            locations.Locations = _metroSupport.LocationRepository.GetAllLocations().ToPagedList(currentpage, 10);
            if (locations.Locations == null)
            {
                return HttpNotFound();
            }
            return View(locations);
        }

        [HttpPost]
        public JsonResult LocationInfo(string locationid)
        {
            if (locationid == null)
            {
                return Json(false);
            }
            var location = _metroSupport.LocationRepository.GetLocation(locationid);
            if (location == null)
            {
                return Json(false);
            }
            return Json(location);
        }

    
        public ActionResult RemoveLocation(string locationid)
        {
            if (locationid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool statusresult = _metroSupport.LocationRepository.DeleteLocation(locationid);
            if (!statusresult)
            {
                return HttpNotFound();
            }

            return Redirect(HttpContext.Request.UrlReferrer.PathAndQuery); 
        }

        [HttpPost]
        public JsonResult CreateLocation(Location location)
        {
            bool statment = false;
            if (ModelState.IsValid)
            {
                statment = _metroSupport.LocationRepository.CreateNewLocation(location);

            }
            return Json(statment);
        }
    }
}
