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
    public class DepartmentsController : Controller
    {
        //
        // GET: /Boss/

       private readonly MetroSupportService _metroSupport;
       public DepartmentsController(MetroSupportService metroSupport)
        {
            _metroSupport = metroSupport;
        }

       [HttpGet]
        public ActionResult It(int? page)
        {
            int currentpage = (int) (page.HasValue ? page - 1 : 0);
            DepartmentViewModel department = new DepartmentViewModel();
            department.Departments = _metroSupport.DepartmentRepository.GetDepartmentsByName("It").ToPagedList(currentpage, 10);
            if (department.Departments == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

       [HttpGet]
       public ActionResult Svyaz(int? page)
       {
           int currentpage = (int)(page.HasValue ? page - 1 : 0);
           DepartmentViewModel department = new DepartmentViewModel();
           department.Departments = _metroSupport.DepartmentRepository.GetDepartmentsByName("Svyaz").ToPagedList(currentpage, 10);
           if (department.Departments == null)
           {
               return HttpNotFound();
           }
           return View(department);
       }
       [HttpGet]
       public ActionResult Aspp(int? page)
       {
           int currentpage = (int)(page.HasValue ? page - 1 : 0);
           DepartmentViewModel department = new DepartmentViewModel();
           department.Departments = _metroSupport.DepartmentRepository.GetDepartmentsByName("Aspp").ToPagedList(currentpage, 10);
           if (department.Departments == null)
           {
               return HttpNotFound();
           }
           return View(department);
       }

       public ActionResult Pa(int? page)
       {
           int currentpage = (int)(page.HasValue ? page - 1 : 0);
           DepartmentViewModel department = new DepartmentViewModel();
           department.Departments = _metroSupport.DepartmentRepository.GetDepartmentsByName("Pa").ToPagedList(currentpage, 10);
           if (department.Departments == null)
           {
               return HttpNotFound();
           }
           return View(department);
       }
        
        
        [HttpPost]
        public JsonResult DepartmentInfo(string departmentid)
        {
            if (departmentid == null)
            {
                return Json(null);
            }
            var department = _metroSupport.DepartmentRepository.GetDepartmentById(departmentid);
            if (department == null)
            {
                return Json(null);
            }
            return Json(department);
        }

    
        public ActionResult RemoveDepartment(string departmentid)
        {
            if (departmentid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool statusresult = _metroSupport.DepartmentRepository.DeleteDepartment(departmentid);
            if (!statusresult)
            {
                return HttpNotFound();
            }

            return Redirect(HttpContext.Request.UrlReferrer.PathAndQuery);
        }

        [HttpPost]
        public JsonResult CreateDepartment(Department department)
        {
            bool statement = false;
            if (ModelState.IsValid)
            {
                 statement = department.DepartmentId == Guid.Empty ? _metroSupport.DepartmentRepository.CreateNewDepartment(department) : false;

            }
            return Json(statement);
        }

    }
}
