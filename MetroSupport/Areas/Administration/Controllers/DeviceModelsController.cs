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
    public class DeviceModelsController : Controller
    {
        private readonly MetroSupportService _metroSupport;
        public DeviceModelsController(MetroSupportService metroSupport)
        {
            _metroSupport = metroSupport;
        }

       [HttpGet]
        public ActionResult It(int? page)
        {
            int currentpage = (int) (page.HasValue ? page - 1 : 0);
            DeviceModelsViewModel devicemodel = new DeviceModelsViewModel(); 
            devicemodel.DeviceModels = _metroSupport.DeviceModelRepository.GetDevicesModelsByDepartment("It").ToPagedList(currentpage, 10);
            if (devicemodel.DeviceModels == null)
            {
                return HttpNotFound();
            }
            return View(devicemodel);
        }

        [HttpGet]
        public ActionResult Svyaz(int? page)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            DeviceModelsViewModel devicemodel = new DeviceModelsViewModel();
            devicemodel.DeviceModels = _metroSupport.DeviceModelRepository.GetDevicesModelsByDepartment("Svyaz").ToPagedList(currentpage, 10);
            if (devicemodel.DeviceModels == null)
            {
                return HttpNotFound();
            }
            return View(devicemodel);
        }

        [HttpGet]
        public ActionResult Pa(int? page)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            DeviceModelsViewModel devicemodel = new DeviceModelsViewModel();
            devicemodel.DeviceModels = _metroSupport.DeviceModelRepository.GetDevicesModelsByDepartment("Pa").ToPagedList(currentpage, 10);
            if (devicemodel.DeviceModels == null)
            {
                return HttpNotFound();
            }
            return View(devicemodel);
        }
 
        [HttpGet]
        public ActionResult Aspp(int? page)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            DeviceModelsViewModel devicemodel = new DeviceModelsViewModel();
            devicemodel.DeviceModels = _metroSupport.DeviceModelRepository.GetDevicesModelsByDepartment("Aspp").ToPagedList(currentpage, 10);
            if (devicemodel.DeviceModels == null)
            {
                return HttpNotFound();
            }
            return View(devicemodel);
        }
        
        [HttpPost]
        public JsonResult DeviceModelInfo(string modelid)
        {
            if (modelid == null)
            {
                return Json(false);
            }
            var devicemodel = _metroSupport.DeviceModelRepository.GetDeviceModelById(modelid);
            if (devicemodel == null)
            {
                return Json(false);
            }
            return Json(devicemodel);
        }

        
        public ActionResult RemoveDeviceModel(string modelid)
        {
            if (modelid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool statusresult = _metroSupport.DeviceModelRepository.DeleteDeviceModel(modelid);
            if (!statusresult)
            {
                return HttpNotFound();
            }

            return Redirect(HttpContext.Request.UrlReferrer.PathAndQuery); 
        }

        [HttpPost]
        public JsonResult CreateModel(DeviceModel devicemodel)
        {
            bool statement = false;
            if (ModelState.IsValid)
            {
                statement = devicemodel.ModelId == Guid.Empty ? _metroSupport.DeviceModelRepository.CreateNewDeviceModel(devicemodel) : _metroSupport.DeviceModelRepository.EditDeviceModel(devicemodel);
                
            }
            return Json(statement);
        }

        [HttpPost]
        public JsonResult GetModelIndexators()
        {
            var models = _metroSupport.ModelIndexatorRepository.GetAllIndexators();
            if (models == null)
            {
                return Json(false);
            }
            return Json(models);
        }

    }
}
