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
    public class BossesController : Controller
    {
        //
        // GET: /Boss/

       private readonly MetroSupportService _metroSupport;
       public BossesController(MetroSupportService metroSupport)
        {
            _metroSupport = metroSupport;
        }

       [HttpGet]
        public ActionResult It(int? page)
        {
            int currentpage = (int) (page.HasValue  ? page - 1 : 0);
            BossViewModel bosses = new BossViewModel();
            bosses.Bosses = _metroSupport.BossRepository.GetBossesByDepartment("IT").ToPagedList(currentpage, 10);
            if (bosses.Bosses == null)
            {
                return HttpNotFound();
            }
            return View(bosses);
        }

        [HttpGet]
        public ActionResult Svyaz(int? page)
        {

            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            BossViewModel bosses = new BossViewModel();
            bosses.Bosses = _metroSupport.BossRepository.GetBossesByDepartment("Svyaz").ToPagedList(currentpage, 10);
            if (bosses.Bosses == null)
            {
                return HttpNotFound();
            }
            return View(bosses);
        }

        [HttpGet]
        public ActionResult Pa(int? page)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            BossViewModel bosses = new BossViewModel();
            bosses.Bosses = _metroSupport.BossRepository.GetBossesByDepartment("Pa").ToPagedList(currentpage, 10);
            if (bosses.Bosses == null)
            {
                return HttpNotFound();
            }
            return View(bosses);
        }
 
        [HttpGet]
        public ActionResult Aspp(int? page)
        {
            int currentpage = (int)(page.HasValue ? page - 1 : 0);
            BossViewModel bosses = new BossViewModel();
            bosses.Bosses = _metroSupport.BossRepository.GetBossesByDepartment("Aspp").ToPagedList(currentpage, 10);
            if (bosses.Bosses == null)
            {
                return HttpNotFound();
            }
            return View(bosses);
        }
        
        [HttpPost]
        public JsonResult BossInfo(string bossid)
        {
            if (bossid == null)
            {
                return Json(false);
            }
            var assigner = _metroSupport.AssignerRepository.GetAssignerById(bossid);
            if (assigner == null)
            {
                return Json(false);
            }
            return Json(assigner);
        }

       
        public ActionResult RemoveBoss(string bossid)
        {
            if (bossid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool statusresult = _metroSupport.BossRepository.DeleteBoss(bossid);
            if (!statusresult)
            {
                return HttpNotFound();
            }

            return Redirect(HttpContext.Request.UrlReferrer.PathAndQuery);
        }

        [HttpPost]
        public JsonResult CreateBoss(AssignBoss boss)
        {
            bool statment = false;
            if (ModelState.IsValid)
            {
                statment = boss.BossId == Guid.Empty ? _metroSupport.BossRepository.CreateNewBoss(boss) : _metroSupport.BossRepository.SaveBossChanges(boss);
            }
            return Json(statment);
        }

    }
}
