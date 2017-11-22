using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MetroSupport.Areas.Administration.ViewModels;
using MetroSupport.BLL.Services;
using MetroSupport.Commons;
using MetroSupport.Models;
using MvcPaging;

namespace MetroSupport.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ThemesController : Controller
    {
        //
        // GET: /Theme/

       private readonly MetroSupportService _metroSupport;
       public ThemesController(MetroSupportService metroSupport)
        {
            _metroSupport = metroSupport;
        }

       [HttpGet]
        public ActionResult IT(int? page)
        {
            int currentpage = (int) (page.HasValue ? page - 1 : 0);
            ThemeViewModel themes = new ThemeViewModel();
            themes.TroubleSubjects = _metroSupport.TroubleSubjectRepository.GetTroubleSubjectsByDepartment("IT").ToPagedList(currentpage, 10);
            if (themes.TroubleSubjects == null)
            {
                return HttpNotFound();
            }
            return View(themes);
        }

       [HttpGet]
       public ActionResult Svyaz(int? page)
       {
           int currentpage = (int)(page.HasValue ? page - 1 : 0);
           ThemeViewModel themes = new ThemeViewModel();
           themes.TroubleSubjects = _metroSupport.TroubleSubjectRepository.GetTroubleSubjectsByDepartment("Svyaz").ToPagedList(currentpage, 10);
           if (themes.TroubleSubjects == null)
           {
               return HttpNotFound();
           }
           return View(themes);
       }
       [HttpGet]
       public ActionResult Aspp(int? page)
       {
           int currentpage = (int)(page.HasValue ? page - 1 : 0);
           ThemeViewModel themes = new ThemeViewModel();
           themes.TroubleSubjects = _metroSupport.TroubleSubjectRepository.GetTroubleSubjectsByDepartment("Aspp").ToPagedList(currentpage, 10);
           if (themes.TroubleSubjects == null)
           {
               return HttpNotFound();
           }
           return View(themes);
       }

       public ActionResult Pa(int? page)
       {
           int currentpage = (int)(page.HasValue ? page - 1 : 0);
           ThemeViewModel themes = new ThemeViewModel();
           themes.TroubleSubjects = _metroSupport.TroubleSubjectRepository.GetTroubleSubjectsByDepartment("Pa").ToPagedList(currentpage, 10);
           if (themes.TroubleSubjects == null)
           {
               return HttpNotFound();
           }
           return View(themes);
       }
        
        
        [HttpPost]
        public JsonResult ThemeInfo(string themeid)
        {
            if (themeid == null)
            {
                return Json(false);
            }
            var themes = _metroSupport.TroubleSubjectRepository.GetTroubleSubjectById(themeid);
            if (themes == null)
            {
                return Json(false);
            }
            return Json(themes);
        }

     
        public ActionResult RemoveTheme(string themeid)
        {
            if (themeid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool statusresult = _metroSupport.TroubleSubjectRepository.DeleteTroubleSubject(themeid);
            if (!statusresult)
            {
                return HttpNotFound();
            }

            return Redirect(HttpContext.Request.UrlReferrer.PathAndQuery);
        }

        [HttpPost]
        public JsonResult CreatTheme(TroubleSubject theme)
        {
            bool statment = false;
            if (ModelState.IsValid)
            {
                statment = theme.SubjectId == Guid.Empty ? _metroSupport.TroubleSubjectRepository.CreateNewTroubleSubject(theme): _metroSupport.TroubleSubjectRepository.SaveTroubleSubject(theme);
                
            }
            return Json(statment);
        }

    }
}
