using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using MetroSupport.BLL.Implement.Export;
using MetroSupport.BLL.Interfaces;
using MetroSupport.Commons;
using MetroSupport.BLL.Services;
using MetroSupport.Models;
using MetroSupport.ViewModels;
using Omu.Awem;
using Omu.AwesomeMvc;


namespace MetroSupport.Controllers
{
     [Authorize(Roles = "SvyazWork, SvyazBoss, CallCenter, Administrator")]
    public class SvyazCallRequestListController : Controller
    {

         private MetroSupportService _metroSupport;
        public SvyazCallRequestListController(MetroSupportService metroSupport)
        {
            _metroSupport = metroSupport;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OpenCallRequests()
        {
            return View();
        }
        public async Task<ActionResult> OpenCallRequestsData(GridParams g)
        {
            Task<IQueryable<SvyazCallRequest>> callrequesttask = _metroSupport.SvyazCallRepository.GetAllOpenRequestsAsync();
            await Task.WhenAll(callrequesttask);
            IQueryable<SvyazCallRequest> callrequests = callrequesttask.Result.AsQueryable();


            var gridbuilder = new GridModelBuilder<SvyazCallRequest>(callrequests, g)
            {
                Key = "CallRequestId",
                Map = o => new
                {
                    o.CallRequestId,
                    CreationDate = o.Creation.Value.ToShortDateString(),
                    CreationTime = o.Time.Value.Hour + ":" + o.Time.Value.Minute + ":" + o.Time.Value.Second,
                    o.RequestNumber,
                    RequestStatus = Status.StatusConvertion(o.Status, o.IsWorkingOn),
                    o.RequestorName,
                    o.AssignBoss,
                    o.AssignTo,
                    o.Category,
                    o.TroubleSubject,
                    o.Comment,
                }
            }.Build();

            return Json(gridbuilder);
        }

        public ActionResult OpenCallRequestsByDate()
        {
            return View();
        }
        public async Task<ActionResult> OpenCallRequestsByDateData(GridParams gridparams, bool collapsed = true)
        {

            Task<IQueryable<SvyazCallRequest>> task = _metroSupport.SvyazCallRepository.GetAllOpenRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<SvyazCallRequest> callrequests = task.Result.AsQueryable();

            return Json(BuildGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult OpenCallRequestsByAssignerAndDate()
        {

            return View();
        }

        public async Task<ActionResult> OpenCallRequestsByAssignerAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<SvyazCallRequest>> task = _metroSupport.SvyazCallRepository.GetAllOpenRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<SvyazCallRequest> callrequests = task.Result;
            if (callrequests == null)
            {
                return HttpNotFound();
            }
            return Json(BuildGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult OpenCallRequestsByBossAndDate()
        {
            return View();
        }

        public async Task<ActionResult> OpenCallRequestsByBossAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<SvyazCallRequest>> task = _metroSupport.SvyazCallRepository.GetAllOpenRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<SvyazCallRequest> callrequests = task.Result;
            if (callrequests == null)
            {
                return HttpNotFound();
            }
            return Json(BuildGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult AllCallRequests()
        {

            return View();
        }

        public async Task<ActionResult> AllCallRequestsData(GridParams gridparams)
        {
            Task<IQueryable<SvyazCallRequest>> task = _metroSupport.SvyazCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<SvyazCallRequest> callrequests = task.Result.AsQueryable();

            var gridbuilder = new GridModelBuilder<SvyazCallRequest>(callrequests.AsQueryable(), gridparams)
            {
                Key = "CallRequestId",
                Map = o => new
                {
                    o.CallRequestId,
                    CreationDate = o.Creation.Value.ToShortDateString(),
                    CreationTime = o.Time.Value.Hour + ":" + o.Time.Value.Minute + ":" + o.Time.Value.Second,
                    o.RequestNumber,
                    RequestStatus = Status.StatusConvertion(o.Status, o.IsWorkingOn),
                    o.RequestorName,
                    o.AssignBoss,
                    o.AssignTo,
                    o.Category,
                    o.TroubleSubject,
                    o.Comment,
                }
            }.Build();

            return Json(gridbuilder);
        }

        public ActionResult AllCallRequestsByDate()
        {
            return View();
        }


        public async Task<ActionResult> AllCallRequestsByDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<SvyazCallRequest>> task = _metroSupport.SvyazCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<SvyazCallRequest> callrequests = task.Result.AsQueryable();


            return Json(BuildGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult AllCallRequestsByDateInProduce()
        {
            return View();
        }
        public async Task<ActionResult> AllCallRequestsByDateInProduceData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<SvyazCallRequest>> task = _metroSupport.SvyazCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<SvyazCallRequest> callrequests = task.Result.AsQueryable();

            return Json(BuildGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult AllCallRequestsByAssignerAndDate()
        {
            return View();
        }

        public async Task<ActionResult> AllCallRequestsByAssignerAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<SvyazCallRequest>> task = _metroSupport.SvyazCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<SvyazCallRequest> callrequests = task.Result.AsQueryable();


            return Json(BuildGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult AllCallRequestsByBossAndDate()
        {
            return View();
        }

        public async Task<ActionResult> AllCallRequestsByBossAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<SvyazCallRequest>> task = _metroSupport.SvyazCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<SvyazCallRequest> callrequests = task.Result.AsQueryable();


            return Json(BuildGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult AllCallRequestsByStatus()
        {
            return View();
        }

        public async Task<ActionResult> AllCallRequestsByStatusData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<SvyazCallRequest>> task = _metroSupport.SvyazCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<SvyazCallRequest> callrequests = task.Result.AsQueryable();

            return Json(new GridModelBuilder<SvyazCallRequest>(callrequests, gridparams)
            {
                Key = "CallRequestId",
                MakeHeader = mh =>
                {
                    var firstitem = mh.Items.First();
                    var val = string.Join(" ", AweUtil.GetColumnValue(mh.Column, firstitem).Select(o => o.ToStatusGridStr()));
                    var count = mh.Items.Count();
                    return new GroupHeader
                    {
                        Content = string.Format("{0} ({1})", val, count),
                        Collapsed = collapsed
                    };

                },
                Map = o => new
                {
                    o.CallRequestId,
                    CreationDate = o.Creation.Value.ToShortDateString(),
                    CreationTime = o.Time.Value.Hour + ":" + o.Time.Value.Minute + ":" + o.Time.Value.Second,
                    o.RequestNumber,
                    o.Status,
                    o.RequestorName,
                    o.AssignBoss,
                    o.AssignTo,
                    o.Category,
                    o.TroubleSubject,
                    o.Comment,
                }
            }.Build());
        }

        public ActionResult AllCallRequestsByRequestor()
        {
            return View();
        }
        public async Task<ActionResult> AllCallRequestsByRequestorData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<SvyazCallRequest>> task = _metroSupport.SvyazCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<SvyazCallRequest> callrequests = task.Result.AsQueryable();

            return Json(BuildGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult NewCallRequest()
        {
            return RedirectToAction("Index", "SvyazCallRequest");
        }

        [HttpPost]
        public ActionResult SimpleSearch(string searchvalue)
        {
            ViewBag.SearchValue = searchvalue;
            return View("SimpleSearch");
        }

        public ActionResult SimpleSearchData(GridParams g, string searchvalue)
        {

            if (String.IsNullOrWhiteSpace(searchvalue))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IQueryable<SearchResultModel> result = _metroSupport.MetroSearch.SimpleSearch(SearchEntry.Svyaz, searchvalue);

            var gridbuilder = new GridModelBuilder<SearchResultModel>(result, g)
            {
                Key = "CallRequestId",
                Map = o => new
                {
                    o.CallRequestId,
                    CreationDate = o.Creation.Value.ToShortDateString(),
                    CreationTime = o.Time.Value.Hour + ":" + o.Time.Value.Minute + ":" + o.Time.Value.Second,
                    o.RequestNumber,
                    RequestStatus = o.Status,
                    o.RequestorName,
                    o.AssignBoss,
                    o.AssignTo,
                    o.Category,
                    o.TroubleSubject,
                    o.Comment,
                }
            }.Build();

            return Json(gridbuilder);
        }

        [HttpPost]
        public ActionResult AdvanceSearch(FilterViewModel filtermodel)
        {
            if (filtermodel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            IQueryable<SearchResultModel> callrequests = _metroSupport.MetroSearch.AdvaceSearch(SearchEntry.Svyaz, filtermodel).Take(100);
            List<SearchResultModel> search = callrequests.ToList();
            if (callrequests == null)
            {
                return HttpNotFound();
            }
            return View("AdvanceSearch", callrequests);
        }


        [HttpPost]
        public JsonResult DeleteCallRequests(string selectedIds)
        {
            if (String.IsNullOrWhiteSpace(selectedIds))
            {
                return Json(false);
            }
            string[] requestIds = Regex.Split(selectedIds, ";");
            bool statement = _metroSupport.SvyazCallRepository.DeleteCallRequest(requestIds);
            if (!statement)
            {
                return Json(false);
            }
            return Json(statement);
        }

        public FileResult Export(string ids)
        {

            return File("", "");
        }

        [HttpPost]
      
        public FileResult Report(FilterViewModel filtermodel)
        {
            if (filtermodel == null)
            {
                return File("", "");
            }
           
            List<ExportResultModel> result = _metroSupport.DataFilter.Filter(SearchEntry.Svyaz, filtermodel).ToList();
            return File(_metroSupport.DataExport.ExportTo(new MsExcel(), result.ListToDataTable()), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
        }


        private GridModelDto<SvyazCallRequest> BuildGridParameters(IQueryable<SvyazCallRequest> callrequests, GridParams g,
            bool collapsed)
        {

            return new GridModelBuilder<SvyazCallRequest>(callrequests, g)
            {
                Key = "CallRequestId",
                MakeHeader = mh =>
                {
                    var firstitem = mh.Items.First();
                    var val = string.Join(" ", AweUtil.GetColumnValue(mh.Column, firstitem).Select(o => o.ToDateTimeGridStr()));
                    var count = mh.Items.Count();
                    return new GroupHeader
                    {
                        Content = string.Format("{0} ({1})", val, count),
                        Collapsed = collapsed
                    };

                },
                Map = o => new
                {
                    o.CallRequestId,
                    CreationDate = o.Creation.Value.ToShortDateString(),
                    CreationTime = o.Time.Value.Hour + ":" + o.Time.Value.Minute + ":" + o.Time.Value.Second,
                    o.RequestNumber,
                    RequestStatus = Status.StatusConvertion(o.Status, o.IsWorkingOn),
                    o.RequestorName,
                    o.AssignBoss,
                    o.AssignTo,
                    o.Category,
                    o.TroubleSubject,
                    o.Comment,
                }
            }.Build();

        }
        
         [HttpPost]
        
        public JsonResult GetSvyazThemes()
        {

            IQueryable<TroubleSubject> themes = _metroSupport.TroubleSubjectRepository.GetTroubleSubjectsByDepartment("Svyaz");
            if (themes == null)
            {
                return Json(null);

            }
            return Json(themes);
        }

         [HttpPost]
         public JsonResult GetPeople(string prefix)
         {
             if (String.IsNullOrWhiteSpace(prefix))
             {
                 return Json(null);
             }
             IQueryable<RequestOwner> requestors = _metroSupport.RequestOwnerRepository.GetRequestOwnersByName(prefix).Take(5);
             if (requestors == null)
             {
                 return Json(null);

             }
             return Json(requestors, JsonRequestBehavior.AllowGet);
         }

    }
}
