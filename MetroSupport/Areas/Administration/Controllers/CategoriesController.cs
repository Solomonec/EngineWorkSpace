using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MetroSupport.Areas.Administration.ViewModels;
using MetroSupport.BLL.Services;
using MetroSupport.Models;
using Microsoft.Ajax.Utilities;
using MvcPaging;

namespace MetroSupport.Areas.Administration.Controllers
{
     [Authorize(Roles = "Administrator")]
    public class CategoriesController : Controller
    {
        //
        // GET: /Categories/

       private readonly MetroSupportService _metroSupport;
       public CategoriesController(MetroSupportService metroSupport)
        {
            _metroSupport = metroSupport;
        }

       [HttpGet]
        public ActionResult It(int? page)
        {
            int currentpage = (int) (page.HasValue ? page - 1 : 0);
            ItCategoryViewModel categories = new ItCategoryViewModel();

            categories.ItCategories  = _metroSupport.ItCategoryRepository.GetAllCategories().ToPagedList(currentpage, 10);
            if (categories.ItCategories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        [HttpGet]
        public ActionResult Svyaz(int? page)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            SvyazCategoryViewModel categories = new SvyazCategoryViewModel();

            categories.SvyazCategories = _metroSupport.SvyazCategoryRepository.GetAllCategories().ToPagedList(currentpage, 10);
            if (categories.SvyazCategories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }

        [HttpGet]
        public ActionResult Pa(int? page)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            PaCategoryViewModel categories = new PaCategoryViewModel();
            categories.PaCategories = _metroSupport.PaCategoryRepository.GetAllCategories().ToPagedList(currentpage, 10);
            if (categories.PaCategories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }
 
        [HttpGet]
        public ActionResult Aspp(int? page)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            AsppCategoryViewModel categories = new AsppCategoryViewModel();
            categories.AsppCategories = _metroSupport.AsppCategoryRepository.GetAllCategories().ToPagedList(currentpage, 10);
            if (categories.AsppCategories == null)
            {
                return HttpNotFound();
            }
            return View(categories);
        }
        
        [HttpPost]
        public JsonResult ItCategoryInfo(string categoryid)
        {
            if (categoryid == null)
            {
                return Json(null);
            }
            var category = _metroSupport.ItCategoryRepository.GetCategoryById(categoryid);
            if (category == null)
            {
                return Json(null);
            }
            return Json(category);
        }

        [HttpPost]
        public JsonResult AsppCategoryInfo(string categoryid)
        {
            if (categoryid == null)
            {
                return Json(null);
            }
            var category = _metroSupport.AsppCategoryRepository.GetCategoryById(categoryid);
            if (category == null)
            {
                return Json(null);
            }
            return Json(category);
        }

        [HttpPost]
        public JsonResult SvyazCategoryInfo(string categoryid)
        {
            if (categoryid == null)
            {
                return Json(null);
            }
            var category = _metroSupport.SvyazCategoryRepository.GetCategoryById(categoryid);
            if (category == null)
            {
                return Json(null);
            }
            return Json(category);
        }

        [HttpPost]
        public JsonResult PaCategoryInfo(string categoryid)
        {
            if (categoryid == null)
            {
                return Json(null);
            }
            var category = _metroSupport.PaCategoryRepository.GetCategoryById(categoryid);
            if (category == null)
            {
                return Json(null);
            }
            return Json(category);
        }

        [HttpGet]
        public ActionResult RemoveItCategory(string categoryid)
        {
            if (categoryid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool statusresult = _metroSupport.ItCategoryRepository.DeleteCategory(categoryid);
            if (!statusresult)
            {
                return HttpNotFound();
            }

            return Redirect(HttpContext.Request.UrlReferrer.PathAndQuery); 
        }

        [HttpGet]
        public ActionResult RemoveAsppCategory(string categoryid)
        {
            if (categoryid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool statusresult = _metroSupport.AsppCategoryRepository.DeleteCategory(categoryid);
            if (!statusresult)
            {
                return HttpNotFound();
            }

            return Redirect(HttpContext.Request.UrlReferrer.PathAndQuery);
        }

        [HttpGet]
        public ActionResult RemoveSvyazCategory(string categoryid)
        {
            if (categoryid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool statusresult = _metroSupport.SvyazCategoryRepository.DeleteCategory(categoryid);
            if (!statusresult)
            {
                return HttpNotFound();
            }

            return Redirect(HttpContext.Request.UrlReferrer.PathAndQuery);
        }

        [HttpGet]
        public ActionResult RemovePaCategory(string categoryid)
        {
            if (categoryid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool statusresult = _metroSupport.PaCategoryRepository.DeleteCategory(categoryid);
            if (!statusresult)
            {
                return HttpNotFound();
            }

            return Redirect(HttpContext.Request.UrlReferrer.PathAndQuery);
        }

        [HttpPost]
        public JsonResult CreateItCategory(ItCategory category)
        {
            bool statment = false;
            if (ModelState.IsValid)
            {
                statment = category.CategoryId == Guid.Empty ? _metroSupport.ItCategoryRepository.CreateNewCategory(category) : _metroSupport.ItCategoryRepository.SaveCategoryChanges(category);
                
            }
            return Json(statment);
        }

         [HttpPost]
        public JsonResult CreateAsppCategory(AsppCategory category)
        {
            bool statment = false;
            if (ModelState.IsValid)
            {
                statment = category.CategoryId == Guid.Empty ? _metroSupport.AsppCategoryRepository.CreateNewCategory(category) : _metroSupport.AsppCategoryRepository.SaveCategoryChanges(category);

            }
            return Json(statment);
        }

         [HttpPost]
         public JsonResult CreateSvyazCategory(SvyazCategory category)
        {
            bool statment = false;
            if (ModelState.IsValid)
            {
                statment = category.CategoryId == Guid.Empty ? _metroSupport.SvyazCategoryRepository.CreateNewCategory(category) : _metroSupport.SvyazCategoryRepository.SaveCategoryChanges(category);

            }
            return Json(statment);
        }

         [HttpPost]
         public JsonResult CreatePaCategory(PaCategory category)
        {
            bool statment = false;
            if (ModelState.IsValid)
            {
                statment = category.CategoryId == Guid.Empty ? _metroSupport.PaCategoryRepository.CreateNewCategory(category) : _metroSupport.PaCategoryRepository.SaveCategoryChanges(category);
                
            }
            return Json(statment);
        }

     
         [HttpPost]
         public JsonResult GetItCategories()
         {
                 IEnumerable<CategoryIndexator> categories = _metroSupport.CategoryIndexatorRepository.GetAllBaseCategories("It");
                 if (categories != null)
                 {
                     return Json(categories);
                 }
                 return Json(null);
         }

         [HttpPost]
         public JsonResult GetAllItCategories()
         {
             IEnumerable<CategoryIndexator> categories = _metroSupport.CategoryIndexatorRepository.GetIndexatorsByDepartment("It");
             if (categories != null)
             {
                 return Json(categories);
             }
             return Json(null);
         }

         [HttpPost]
         public JsonResult GetItSubCategories()
         {
             IEnumerable<CategoryIndexator> categories = _metroSupport.CategoryIndexatorRepository.GetAllSubCategories("It");
            
             if (categories != null)
             {
                 return Json(categories);
             }
             return Json(null);
         }

         [HttpPost]
         public JsonResult GetAllSvyazCategories()
         {
             IEnumerable<CategoryIndexator> categories = _metroSupport.CategoryIndexatorRepository.GetIndexatorsByDepartment("Svyaz");
             if (categories != null)
             {
                 return Json(categories);
             }
             return Json(null);
         }

         [HttpPost]
         public JsonResult GetSvyazCategories()
         {
             IEnumerable<CategoryIndexator> categories = _metroSupport.CategoryIndexatorRepository.GetAllBaseCategories("Svyaz");
             if (categories != null)
             {
                 return Json(categories);
             }
             return Json(null);
         }

         [HttpPost]
         public JsonResult GetSvyazSubCategories()
         {
             IEnumerable<CategoryIndexator> categories = _metroSupport.CategoryIndexatorRepository.GetAllSubCategories("Svyaz");
             if (categories != null)
             {
                 return Json(categories);
             }
             return Json(null);
         }

         [HttpPost]
         public JsonResult GetAllAsppCategories()
         {
             IEnumerable<CategoryIndexator> categories = _metroSupport.CategoryIndexatorRepository.GetIndexatorsByDepartment("Aspp");
             if (categories != null)
             {
                 return Json(categories);
             }
             return Json(null);
         }

         [HttpPost]
         public JsonResult GetAsppCategories()
         {
             IEnumerable<CategoryIndexator> categories = _metroSupport.CategoryIndexatorRepository.GetAllBaseCategories("Aspp");
             if (categories != null)
             {
                 return Json(categories);
             }
             return Json(null);
         }

         [HttpPost]
         public JsonResult GetAsppSubCategories()
         {
             IEnumerable<CategoryIndexator> categories = _metroSupport.CategoryIndexatorRepository.GetAllSubCategories("Aspp");
             if (categories != null)
             {
                 return Json(categories);
             }
             return Json(null);
         }

         [HttpPost]
         public JsonResult GetAllPaCategories()
         {
             IEnumerable<CategoryIndexator> categories = _metroSupport.CategoryIndexatorRepository.GetIndexatorsByDepartment("Pa");
             if (categories != null)
             {
                 return Json(categories);
             }
             return Json(null);
         }

         [HttpPost]
         public JsonResult GetPaCategories()
         {
             IEnumerable<CategoryIndexator> categories = _metroSupport.CategoryIndexatorRepository.GetAllBaseCategories("Pa");
             if (categories != null)
             {
                 return Json(categories);
             }
             return Json(null);
         }

         [HttpPost]
         public JsonResult GetPaSubCategories()
         {
             IEnumerable<CategoryIndexator> categories = _metroSupport.CategoryIndexatorRepository.GetAllSubCategories("Pa");
             if (categories != null)
             {
                 return Json(categories);
             }
             return Json(null);
         }

    }
}
