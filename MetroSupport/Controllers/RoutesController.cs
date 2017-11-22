using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetroSupport.Controllers
{
    [Authorize]
    public class RoutesController : Controller
    {
        //
        // GET: /Routes/
        
        public ActionResult RouteDetermination()
        {
            if (User.IsInRole("ItWork") || User.IsInRole("ItBoss"))
            {
                return RedirectToAction("OpenCallRequests","ItCallRequestList");
            }
            else
            if (User.IsInRole("PaWork") || User.IsInRole("PaBoss"))
            {
                return RedirectToAction("OpenCallRequests", "PaCallRequestList");
            }
            else
            if (User.IsInRole("AsppWork") || User.IsInRole("AsppBoss"))
            {
                return RedirectToAction("OpenCallRequests", "AsppCallRequestList");
            } 
            else
            if (User.IsInRole("SvyazWork") || User.IsInRole("SvyazBoss"))
            {
                return RedirectToAction("OpenCallRequests", "SvyazCallRequestList");
            }
            else if (User.IsInRole("CallCenter") || User.IsInRole("Administrator"))
            {
                return RedirectToAction("ItOpenCallRequests", "CentralCallRequestList");
            }
            else return Content("Путь не определен");

        }

    }
}
