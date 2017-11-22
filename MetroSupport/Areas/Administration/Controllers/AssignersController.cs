using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using MetroSupport.Areas.Administration.ViewModels;
using MetroSupport.Models;
using MetroSupport.BLL.Services;
using MvcPaging;

namespace MetroSupport.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AssignersController : Controller
    {
        private readonly MetroSupportService _metroSupport;
        public AssignersController(MetroSupportService metroSupport)
        {
            _metroSupport = metroSupport;
        }

       [HttpGet]
        public ActionResult It(int? page)
        {
            int currentpage = (int) (page.HasValue ? page - 1 : 0);
            AssignerViewModel assigners = new AssignerViewModel();
            assigners.Assigners = _metroSupport.AssignerRepository.GetAssignersByDepartment("It").ToPagedList(currentpage, 10);
            if (assigners.Assigners == null)
            {
                return HttpNotFound();
            }
            return View(assigners);
        }

        [HttpGet]
        public ActionResult Svyaz(int? page)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            AssignerViewModel assigners = new AssignerViewModel();
            assigners.Assigners = _metroSupport.AssignerRepository.GetAssignersByDepartment("Svyaz").ToPagedList(currentpage, 10);
            if (assigners.Assigners == null)
            {
                return HttpNotFound();
            }
            return View(assigners);
        }

        [HttpGet]
        public ActionResult Pa(int? page)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            AssignerViewModel assigners = new AssignerViewModel();
            assigners.Assigners = _metroSupport.AssignerRepository.GetAssignersByDepartment("Pa").ToPagedList(currentpage, 10);
            if (assigners.Assigners == null)
            {
                return HttpNotFound();
            }
            return View(assigners);
        }
 
        [HttpGet]
        public ActionResult Aspp(int? page)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            AssignerViewModel assigners = new AssignerViewModel();
            assigners.Assigners = _metroSupport.AssignerRepository.GetAssignersByDepartment("Aspp").ToPagedList(currentpage, 10);
            if (assigners.Assigners == null)
            {
                return HttpNotFound();
            }
            return View(assigners);
        }
        
        [HttpPost]
        public JsonResult AssignerInfo(string assignerid)
        {
            if (assignerid == null)
            {
                return Json(false);
            }
            var assigner = _metroSupport.AssignerRepository.GetAssignerById(assignerid);
            if (assigner == null)
            {
                return Json(false);
            }
            return Json(assigner);
        }

     
        public ActionResult RemoveAssigner(string assignerid)
        {
            if (assignerid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool statusresult = _metroSupport.AssignerRepository.DeleteAssigner(assignerid);
            if (!statusresult)
            {
                return HttpNotFound();
            }

            return Redirect(HttpContext.Request.UrlReferrer.PathAndQuery);
        }

        [HttpPost]
        public JsonResult CreateAssigner(Assigner assigner)
        {
            bool statment = false;
            if (ModelState.IsValid)
            {
                statment = assigner.AssignerId == Guid.Empty ? _metroSupport.AssignerRepository.CreateNewAssigner(assigner) : _metroSupport.AssignerRepository.SaveAssignerChanges(assigner);
                
            }
            return Json(statment);
        }

        [HttpPost]
        public JsonResult GetDepartmentBosses(string department)
        {
            if (!string.IsNullOrWhiteSpace(department))
            {
                IQueryable<AssignBoss> bosses = _metroSupport.BossRepository.GetBossesByDepartment(department);

                if (bosses == null)
                {
                    return Json(null);
                }
                return Json(bosses);
            }
            return Json(null);
        }



        }
    }
