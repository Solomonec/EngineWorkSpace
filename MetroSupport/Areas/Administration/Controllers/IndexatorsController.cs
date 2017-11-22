using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MetroSupport.Areas.Administration.ViewModels;
using MetroSupport.BLL.Services;
using MetroSupport.Models;
using MvcPaging;

namespace MetroSupport.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class IndexatorsController : Controller
    {
       private readonly MetroSupportService _metroSupport;
       public IndexatorsController(MetroSupportService metroSupport)
        {
            _metroSupport = metroSupport;
        }

       [HttpGet]
        public ActionResult It(int? page)
        {
            int currentpage = (int) (page.HasValue ? page - 1 : 0);
            CategoryIndexatorViewModel indexators = new CategoryIndexatorViewModel();  
            indexators.CategoryIndexators = _metroSupport.CategoryIndexatorRepository.GetIndexatorsByDepartment("It").ToPagedList(currentpage, 10);
            if (indexators.CategoryIndexators == null)
            {
                return HttpNotFound();
            }
            return View(indexators);
        }

        [HttpGet]
        public ActionResult Svyaz(int? page)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            CategoryIndexatorViewModel indexators = new CategoryIndexatorViewModel();
            indexators.CategoryIndexators = _metroSupport.CategoryIndexatorRepository.GetIndexatorsByDepartment("Svyaz").ToPagedList(currentpage, 10);
            if (indexators.CategoryIndexators == null)
            {
                return HttpNotFound();
            }
            return View(indexators);
        }

        [HttpGet]
        public ActionResult Pa(int? page)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            CategoryIndexatorViewModel indexators = new CategoryIndexatorViewModel();
            indexators.CategoryIndexators = _metroSupport.CategoryIndexatorRepository.GetIndexatorsByDepartment("Pa").ToPagedList(currentpage, 10);
            if (indexators.CategoryIndexators == null)
            {
                return HttpNotFound();
            }
            return View(indexators);
        }
 
        [HttpGet]
        public ActionResult Aspp(int? page)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            CategoryIndexatorViewModel indexators = new CategoryIndexatorViewModel();
            indexators.CategoryIndexators = _metroSupport.CategoryIndexatorRepository.GetIndexatorsByDepartment("Aspp").ToPagedList(currentpage, 10);
            if (indexators.CategoryIndexators == null)
            {
                return HttpNotFound();
            }
            return View(indexators);
        }

        [HttpGet]
        public ActionResult Model(int? page)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            ModelIndexatorViewModel indexators = new ModelIndexatorViewModel();
            indexators.ModelIndexators = _metroSupport.ModelIndexatorRepository.GetAllIndexators().ToPagedList(currentpage, 10);
            if (indexators.ModelIndexators == null)
            {
                return HttpNotFound();
            }
            return View(indexators);
        }

        public ActionResult RemoveModelIndexator(string indexatorid)
        {
            if (indexatorid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool statusresult = _metroSupport.ModelIndexatorRepository.DeleteIndexator(indexatorid);
            if (!statusresult)
            {
                return HttpNotFound();
            }

            return Redirect(HttpContext.Request.UrlReferrer.PathAndQuery);
        }
        
        [HttpPost]
        public JsonResult CategoryIndexatorInfo(string indexatorid)
        {
            if (indexatorid == null)
            {
                return Json(false);
            }
            var indexator = _metroSupport.CategoryIndexatorRepository.GetIndexatorsById(indexatorid);
            if (indexator == null)
            {
                return Json(false);
            }
            return Json(indexator);
        }


        public ActionResult RemoveCategoryIndexator(string indexatorid)
        {
            if (indexatorid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool statusresult = _metroSupport.CategoryIndexatorRepository.DeleteIndexator(indexatorid);
            if (!statusresult)
            {
                return HttpNotFound();
            }

            return Redirect(HttpContext.Request.UrlReferrer.PathAndQuery); 
        }

        [HttpPost]
        public JsonResult CategoryIndexator(CategoryIndexator indexator)
        {
            bool statment = false;
            if (ModelState.IsValid)
            {
                statment = indexator.IndexatorId == Guid.Empty ? _metroSupport.CategoryIndexatorRepository.CreateNewIndexator(indexator) : _metroSupport.CategoryIndexatorRepository.SaveIndexator(indexator);
                
            }
            return Json(statment);
        }

        [HttpPost]
        public JsonResult CreateCategoryIndexator(CategoryIndexator indexator)
        {
            bool statment = false;
            if (ModelState.IsValid)
            {
                statment = indexator.IndexatorId == Guid.Empty ? _metroSupport.CategoryIndexatorRepository.CreateNewIndexator(indexator) : _metroSupport.CategoryIndexatorRepository.SaveIndexator(indexator);

            }
            return Json(statment);
        }

        [HttpPost]
        public JsonResult CreateModelIndexator(ModelIndexator indexator)
        {
            bool statment = false;
            if (ModelState.IsValid)
            {
                statment = indexator.IndexatorId == Guid.Empty ? _metroSupport.ModelIndexatorRepository.CreateNewIndexator(indexator) : _metroSupport.ModelIndexatorRepository.SaveIndexator(indexator);

            }
            return Json(statment);
        }

    }
}
