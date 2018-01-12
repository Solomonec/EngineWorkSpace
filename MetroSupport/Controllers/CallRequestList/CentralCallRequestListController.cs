using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MetroSupport.BLL.Implement.Export;
using MetroSupport.BLL.Interfaces;
using MetroSupport.Commons;
using MetroSupport.BLL.Services;
using MetroSupport.Models;
using MetroSupport.ViewModels;
using Omu.Awem;
using Omu.AwesomeMvc;
using System.Threading.Tasks;

namespace MetroSupport.Controllers.CallRequestList
{
     [Authorize(Roles = "CallCenter, Administrator")]
    public class CentralCallRequestListController : Controller
    {
        
        private MetroSupportService _metroSupport;
      
        public CentralCallRequestListController(MetroSupportService metroSupport)
        {
            _metroSupport = metroSupport;
        }

        public ActionResult Index()
        {
            return View("IT/Index");
        }

        public ActionResult ItOpenCallRequests()
        {
            return View("IT/OpenCallRequests");
        }
        public async Task<ActionResult> ItOpenCallRequestsData(GridParams g)
        {
            Task<IQueryable<ItCallRequest>> callrequesttask = _metroSupport.ItCallRepository.GetAllOpenRequestsAsync();
            await Task.WhenAll(callrequesttask);
            IQueryable<ItCallRequest> callrequests = callrequesttask.Result.AsQueryable();


            var gridbuilder = new GridModelBuilder<ItCallRequest>(callrequests, g)
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

        public ActionResult ItOpenCallRequestsByDate()
        {
            return View("IT/OpenCallRequestsByDate");
        }
        public async Task<ActionResult> ItOpenCallRequestsByDateData(GridParams gridparams, bool collapsed = true)
        {

            Task<IQueryable<ItCallRequest>> task = _metroSupport.ItCallRepository.GetAllOpenRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<ItCallRequest> callrequests = task.Result.AsQueryable();

            return Json(BuildItGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult ItOpenCallRequestsByAssignerAndDate()
        {

            return View("IT/OpenCallRequestsByAssignerAndDate");
        }

        public async Task<ActionResult> ItOpenCallRequestsByAssignerAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<ItCallRequest>> task = _metroSupport.ItCallRepository.GetAllOpenRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<ItCallRequest> callrequests = task.Result;
            if (callrequests == null)
            {
                return HttpNotFound();
            }
            return Json(BuildItGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult ItOpenCallRequestsByBossAndDate()
        {
            return View("IT/OpenCallRequestsByBossAndDate");
        }

        public async Task<ActionResult> ItOpenCallRequestsByBossAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<ItCallRequest>> task = _metroSupport.ItCallRepository.GetAllOpenRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<ItCallRequest> callrequests = task.Result;
            if (callrequests == null)
            {
                return HttpNotFound();
            }
            return Json(BuildItGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult ItAllCallRequests()
        {

            return View("IT/AllCallRequests");
        }

        public async Task<ActionResult> ItAllCallRequestsData(GridParams gridparams)
        {
            Task<IQueryable<ItCallRequest>> task = _metroSupport.ItCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<ItCallRequest> callrequests = task.Result.AsQueryable();

            var gridbuilder = new GridModelBuilder<ItCallRequest>(callrequests.AsQueryable(), gridparams)
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

        public ActionResult ItAllCallRequestsByDate()
        {
            return View("IT/AllCallRequestsByDate");
        }


        public async Task<ActionResult> ItAllCallRequestsByDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<ItCallRequest>> task = _metroSupport.ItCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<ItCallRequest> callrequests = task.Result.AsQueryable();


            return Json(BuildItGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult ItAllCallRequestsByDateInProduce()
        {
            return View("IT/AllCallRequestsByDateInProduce");
        }
        public async Task<ActionResult> ItAllCallRequestsByDateInProduceData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<ItCallRequest>> task = _metroSupport.ItCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<ItCallRequest> callrequests = task.Result.AsQueryable();

            return Json(BuildItGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult ItAllCallRequestsByAssignerAndDate()
        {
            return View("IT/AllCallRequestsByAssignerAndDate");
        }

        public async Task<ActionResult> ItAllCallRequestsByAssignerAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<ItCallRequest>> task = _metroSupport.ItCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<ItCallRequest> callrequests = task.Result.AsQueryable();


            return Json(BuildItGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult ItAllCallRequestsByBossAndDate()
        {
            return View("IT/AllCallRequestsByBossAndDate");
        }

        public async Task<ActionResult> ItAllCallRequestsByBossAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<ItCallRequest>> task = _metroSupport.ItCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<ItCallRequest> callrequests = task.Result.AsQueryable();


            return Json(BuildItGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult ItAllCallRequestsByStatus()
        {
            return View("IT/AllCallRequestsByStatus");
        }

        public async Task<ActionResult> ItAllCallRequestsByStatusData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<ItCallRequest>> task = _metroSupport.ItCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<ItCallRequest> callrequests = task.Result.AsQueryable();

            return Json(new GridModelBuilder<ItCallRequest>(callrequests, gridparams)
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

        public ActionResult ItAllCallRequestsByRequestor()
        {
            return View("IT/AllCallRequestsByRequestor");
        }
        public async Task<ActionResult> ItAllCallRequestsByRequestorData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<ItCallRequest>> task = _metroSupport.ItCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<ItCallRequest> callrequests = task.Result.AsQueryable();

            return Json(BuildItGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult NewItCallRequest()
        {
            return RedirectToAction("Index", "ItCallRequest");
        }

        [HttpPost]
        public ActionResult ItSimpleSearch(string searchvalue)
        {
            ViewBag.SearchValue = searchvalue;
            return View("IT/SimpleSearch");
        }

        public ActionResult ItSimpleSearchData(GridParams g, string searchvalue)
        {

            if (String.IsNullOrWhiteSpace(searchvalue))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IQueryable<SearchResultModel> result = _metroSupport.MetroSearch.SimpleSearch(SearchEntry.It, searchvalue);

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
        public ActionResult ItAdvanceSearch(FilterViewModel filtermodel)
        {
            if (filtermodel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SearchResult_FilterViewModel FilterResultModel = new SearchResult_FilterViewModel
            {
                Filter = new FilterViewModel(),
                SearchResult = _metroSupport.MetroSearch.AdvaceSearch(SearchEntry.It, filtermodel).Take(200)
            };

            if (FilterResultModel.SearchResult == null)
            {
                return HttpNotFound();
            }
            return View("IT/AdvanceSearch", FilterResultModel);
           
        }


        [HttpPost]
        public JsonResult DeleteItCallRequests(string selectedIds)
        {
            if (String.IsNullOrWhiteSpace(selectedIds))
            {
                return Json(false);
            }
            string[] requestIds = Regex.Split(selectedIds, ";");
            bool statement = _metroSupport.ItCallRepository.DeleteCallRequest(requestIds);
            if (!statement)
            {
                return Json(false);
            }
            return Json(statement);
        }

        public FileResult ItExport(string ids)
        {

            return File("", "");
        }

        [HttpPost]
        public FileResult ItReport(FilterViewModel filtermodel)
        {
            if (filtermodel == null)
            {
                return File("", "");
            }
            List<SearchResultModel> result = _metroSupport.MetroSearch.AdvaceSearch(SearchEntry.It, filtermodel).ToList();
            return File(_metroSupport.DataExport.ExportTo(new MsExcel(), result.ListToDataTable()), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
        }

        public ActionResult AsppOpenCallRequests()
        {
            return View("Aspp/OpenCallRequests");
        }
        public async Task<ActionResult> AsppOpenCallRequestsData(GridParams g)
        {
            Task<IQueryable<AsppCallRequest>> callrequesttask = _metroSupport.AsppCallRepository.GetAllOpenRequestsAsync();
            await Task.WhenAll(callrequesttask);
            IQueryable<AsppCallRequest> callrequests = callrequesttask.Result.AsQueryable();


            var gridbuilder = new GridModelBuilder<AsppCallRequest>(callrequests, g)
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

        public ActionResult AsppOpenCallRequestsByDate()
        {
            return View("Aspp/OpenCallRequestsByDate");
        }
        public async Task<ActionResult> AsppOpenCallRequestsByDateData(GridParams gridparams, bool collapsed = true)
        {

            Task<IQueryable<AsppCallRequest>> task = _metroSupport.AsppCallRepository.GetAllOpenRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<AsppCallRequest> callrequests = task.Result.AsQueryable();

            return Json(BuildAsppGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult AsppOpenCallRequestsByAssignerAndDate()
        {

            return View("Aspp/OpenCallRequestsByAssignerAndDate");
        }

        public async Task<ActionResult> AsppOpenCallRequestsByAssignerAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<AsppCallRequest>> task = _metroSupport.AsppCallRepository.GetAllOpenRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<AsppCallRequest> callrequests = task.Result;
            if (callrequests == null)
            {
                return HttpNotFound();
            }
            return Json(BuildAsppGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult AsppOpenCallRequestsByBossAndDate()
        {
            return View("Aspp/OpenCallRequestsByBossAndDate");
        }

        public async Task<ActionResult> AsppOpenCallRequestsByBossAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<AsppCallRequest>> task = _metroSupport.AsppCallRepository.GetAllOpenRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<AsppCallRequest> callrequests = task.Result;
            if (callrequests == null)
            {
                return HttpNotFound();
            }
            return Json(BuildAsppGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult AsppAllCallRequests()
        {

            return View("Aspp/AllCallRequests");
        }

        public async Task<ActionResult> AsppAllCallRequestsData(GridParams gridparams)
        {
            Task<IQueryable<AsppCallRequest>> task = _metroSupport.AsppCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<AsppCallRequest> callrequests = task.Result.AsQueryable();

            var gridbuilder = new GridModelBuilder<AsppCallRequest>(callrequests.AsQueryable(), gridparams)
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

        public ActionResult AsppAllCallRequestsByDate()
        {
            return View("Aspp/AllCallRequestsByDate");
        }


        public async Task<ActionResult> AsppAllCallRequestsByDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<AsppCallRequest>> task = _metroSupport.AsppCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<AsppCallRequest> callrequests = task.Result.AsQueryable();


            return Json(BuildAsppGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult AsppAllCallRequestsByDateInProduce()
        {
            return View("Aspp/AllCallRequestsByDateInProduce");
        }
        public async Task<ActionResult> AsppAllCallRequestsByDateInProduceData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<AsppCallRequest>> task = _metroSupport.AsppCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<AsppCallRequest> callrequests = task.Result.AsQueryable();

            return Json(BuildAsppGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult AsppAllCallRequestsByAssignerAndDate()
        {
            return View("Aspp/AllCallRequestsByAssignerAndDate");
        }

        public async Task<ActionResult> AsppAllCallRequestsByAssignerAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<AsppCallRequest>> task = _metroSupport.AsppCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<AsppCallRequest> callrequests = task.Result.AsQueryable();


            return Json(BuildAsppGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult AsppAllCallRequestsByBossAndDate()
        {
            return View("Aspp/AllCallRequestsByBossAndDate");
        }

        public async Task<ActionResult> AsppAllCallRequestsByBossAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<AsppCallRequest>> task = _metroSupport.AsppCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<AsppCallRequest> callrequests = task.Result.AsQueryable();


            return Json(BuildAsppGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult AsppAllCallRequestsByStatus()
        {
            return View("Aspp/AllCallRequestsByStatus");
        }

        public async Task<ActionResult> AsppAllCallRequestsByStatusData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<AsppCallRequest>> task = _metroSupport.AsppCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<AsppCallRequest> callrequests = task.Result.AsQueryable();

            return Json(new GridModelBuilder<AsppCallRequest>(callrequests, gridparams)
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

        public ActionResult AsppAllCallRequestsByRequestor()
        {
            return View("Aspp/AllCallRequestsByRequestor");
        }
        public async Task<ActionResult> AsppAllCallRequestsByRequestorData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<AsppCallRequest>> task = _metroSupport.AsppCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<AsppCallRequest> callrequests = task.Result.AsQueryable();

            return Json(BuildAsppGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult NewAsppCallRequest()
        {
            return RedirectToAction("Index", "AsppCallRequest");
        }

        [HttpPost]
        public ActionResult AsppSimpleSearch(string searchvalue)
        {
            ViewBag.SearchValue = searchvalue;
            return View("Aspp/SimpleSearch");
        }

        public ActionResult AsppSimpleSearchData(GridParams g, string searchvalue)
        {

            if (String.IsNullOrWhiteSpace(searchvalue))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IQueryable<SearchResultModel> result = _metroSupport.MetroSearch.SimpleSearch(SearchEntry.Aspp, searchvalue);

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
        public ActionResult AsppAdvanceSearch(FilterViewModel filtermodel)
        {
            if (filtermodel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SearchResult_FilterViewModel FilterResultModel = new SearchResult_FilterViewModel
            {
                Filter = new FilterViewModel(),
                SearchResult = _metroSupport.MetroSearch.AdvaceSearch(SearchEntry.Aspp, filtermodel).Take(200)
            };

            if (FilterResultModel.SearchResult == null)
            {
                return HttpNotFound();
            }
            return View("Aspp/AdvanceSearch", FilterResultModel);
        }


        [HttpPost]
        public JsonResult DeleteAsppCallRequests(string selectedIds)
        {
            if (String.IsNullOrWhiteSpace(selectedIds))
            {
                return Json(false);
            }
            string[] requestIds = Regex.Split(selectedIds, ";");
            bool statement = _metroSupport.AsppCallRepository.DeleteCallRequest(requestIds);
            if (!statement)
            {
                return Json(false);
            }
            return Json(statement);
        }

        public FileResult AsppExport(string ids)
        {

            return File("", "");
        }

        [HttpPost]
        public FileResult AsppReport(FilterViewModel filtermodel)
        {
            if (filtermodel == null)
            {
                return File("", "");
            }
            List<SearchResultModel> result = _metroSupport.MetroSearch.AdvaceSearch(SearchEntry.Aspp, filtermodel).ToList();
            return File(_metroSupport.DataExport.ExportTo(new MsExcel(), result.ListToDataTable()), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
        }



        public ActionResult SvyazOpenCallRequests()
        {
            return View("Svyaz/OpenCallRequests");
        }
        public async Task<ActionResult> SvyazOpenCallRequestsData(GridParams g)
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
                    o.Comment
                }
            }.Build();

            return Json(gridbuilder);
        }

        public ActionResult SvyazOpenCallRequestsByDate()
        {
            return View("Svyaz/OpenCallRequestsByDate");
        }
        public async Task<ActionResult> OpenCallRequestsByDateData(GridParams gridparams, bool collapsed = true)
        {

            Task<IQueryable<SvyazCallRequest>> task = _metroSupport.SvyazCallRepository.GetAllOpenRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<SvyazCallRequest> callrequests = task.Result.AsQueryable();

            return Json(BuildSvyazGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult SvyazOpenCallRequestsByAssignerAndDate()
        {

            return View("Svyaz/OpenCallRequestsByAssignerAndDate");
        }

        public async Task<ActionResult> SvyazOpenCallRequestsByAssignerAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<SvyazCallRequest>> task = _metroSupport.SvyazCallRepository.GetAllOpenRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<SvyazCallRequest> callrequests = task.Result;
            if (callrequests == null)
            {
                return HttpNotFound();
            }
            return Json(BuildSvyazGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult SvyazOpenCallRequestsByBossAndDate()
        {
            return View("Svyaz/OpenCallRequestsByBossAndDate");
        }

        public async Task<ActionResult> SvyazOpenCallRequestsByBossAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<SvyazCallRequest>> task = _metroSupport.SvyazCallRepository.GetAllOpenRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<SvyazCallRequest> callrequests = task.Result;
            if (callrequests == null)
            {
                return HttpNotFound();
            }
            return Json(BuildSvyazGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult SvyazAllCallRequests()
        {

            return View("Svyaz/AllCallRequests");
        }

        public async Task<ActionResult> SvyazAllCallRequestsData(GridParams gridparams)
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
                    o.Comment
                }
            }.Build();

            return Json(gridbuilder);
        }

        public ActionResult SvyazAllCallRequestsByDate()
        {
            return View("Svyaz/AllCallRequestsByDate");
        }


        public async Task<ActionResult> SvyazAllCallRequestsByDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<SvyazCallRequest>> task = _metroSupport.SvyazCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<SvyazCallRequest> callrequests = task.Result.AsQueryable();


            return Json(BuildSvyazGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult SvyazAllCallRequestsByDateInProduce()
        {
            return View("Svyaz/AllCallRequestsByDateInProduce");
        }
        public async Task<ActionResult> SvyazAllCallRequestsByDateInProduceData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<SvyazCallRequest>> task = _metroSupport.SvyazCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<SvyazCallRequest> callrequests = task.Result.AsQueryable();

            return Json(BuildSvyazGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult SvyazAllCallRequestsByAssignerAndDate()
        {
            return View("Svyaz/AllCallRequestsByAssignerAndDate");
        }

        public async Task<ActionResult> SvyazAllCallRequestsByAssignerAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<SvyazCallRequest>> task = _metroSupport.SvyazCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<SvyazCallRequest> callrequests = task.Result.AsQueryable();


            return Json(BuildSvyazGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult SvyazAllCallRequestsByBossAndDate()
        {
            return View("Svyaz/AllCallRequestsByBossAndDate");
        }

        public async Task<ActionResult> SvyazAllCallRequestsByBossAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<SvyazCallRequest>> task = _metroSupport.SvyazCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<SvyazCallRequest> callrequests = task.Result.AsQueryable();


            return Json(BuildSvyazGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult SvyazAllCallRequestsByStatus()
        {
            return View("Svyaz/AllCallRequestsByStatus");
        }

        public async Task<ActionResult> SvyazAllCallRequestsByStatusData(GridParams gridparams, bool collapsed = true)
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

                    return new GroupHeader
                    {
                        Content = string.Format("{0}", val),
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
                    o.Comment
                }
            }.Build());
        }

        public ActionResult SvyazAllCallRequestsByRequestor()
        {
            return View("Svyaz/AllCallRequestsByRequestor");
        }
        public async Task<ActionResult> SvyazAllCallRequestsByRequestorData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<SvyazCallRequest>> task = _metroSupport.SvyazCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<SvyazCallRequest> callrequests = task.Result.AsQueryable();

            return Json(BuildSvyazGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult NewSvyazCallRequest()
        {
            return RedirectToAction("Index", "SvyazCallRequest");
        }

        [HttpPost]
        public ActionResult SvyazSimpleSearch(string searchvalue)
        {
            ViewBag.SearchValue = searchvalue;
            return View("Svyaz/SimpleSearch");
        }

        public ActionResult SvyazSimpleSearchData(GridParams g, string searchvalue)
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
                    o.Comment
                }
            }.Build();

            return Json(gridbuilder);
        }

        [HttpPost]
        public ActionResult SvyazAdvanceSearch(FilterViewModel filtermodel)
        {
            if (filtermodel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SearchResult_FilterViewModel FilterResultModel = new SearchResult_FilterViewModel
            {
                Filter = new FilterViewModel(),
                SearchResult = _metroSupport.MetroSearch.AdvaceSearch(SearchEntry.Aspp, filtermodel).Take(200)
            };

            if (FilterResultModel.SearchResult == null)
            {
                return HttpNotFound();
            }
            return View("Svyaz/AdvanceSearch", FilterResultModel);
        }


        [HttpPost]
        public JsonResult DeleteSvyazCallRequests(string selectedIds)
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

        public FileResult SvyazExport(string ids)
        {

            return File("", "");
        }

        [HttpPost]
        public FileResult SvyazReport(FilterViewModel filtermodel)
        {
            if (filtermodel == null)
            {
                return File("", "");
            }
            List<SearchResultModel> result = _metroSupport.MetroSearch.AdvaceSearch(SearchEntry.Svyaz, filtermodel).ToList();
            return File(_metroSupport.DataExport.ExportTo(new MsExcel(), result.ListToDataTable()), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
        }

        public ActionResult PaOpenCallRequests()
        {
            return View("Pa/OpenCallRequests");
        }
        public async Task<ActionResult> PaOpenCallRequestsData(GridParams g)
        {
            Task<IQueryable<PaCallRequest>> callrequesttask = _metroSupport.PaCallRepository.GetAllOpenRequestsAsync();
            await Task.WhenAll(callrequesttask);
            IQueryable<PaCallRequest> callrequests = callrequesttask.Result.AsQueryable();


            var gridbuilder = new GridModelBuilder<PaCallRequest>(callrequests, g)
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
                    o.Comment
                }
            }.Build();

            return Json(gridbuilder);
        }

        public ActionResult PaOpenCallRequestsByDate()
        {
            return View("Pa/OpenCallRequestsByDate");
        }
        public async Task<ActionResult> PaOpenCallRequestsByDateData(GridParams gridparams, bool collapsed = true)
        {

            Task<IQueryable<PaCallRequest>> task = _metroSupport.PaCallRepository.GetAllOpenRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<PaCallRequest> callrequests = task.Result.AsQueryable();

            return Json(BuildPaGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult PaOpenCallRequestsByAssignerAndDate()
        {

            return View("Pa/OpenCallRequestsByAssignerAndDate");
        }

        public async Task<ActionResult> PaOpenCallRequestsByAssignerAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<PaCallRequest>> task = _metroSupport.PaCallRepository.GetAllOpenRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<PaCallRequest> callrequests = task.Result;
            if (callrequests == null)
            {
                return HttpNotFound();
            }
            return Json(BuildPaGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult PaOpenCallRequestsByBossAndDate()
        {
            return View("Pa/OpenCallRequestsByBossAndDate");
        }

        public async Task<ActionResult> PaOpenCallRequestsByBossAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<PaCallRequest>> task = _metroSupport.PaCallRepository.GetAllOpenRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<PaCallRequest> callrequests = task.Result;
            if (callrequests == null)
            {
                return HttpNotFound();
            }
            return Json(BuildPaGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult PaAllCallRequests()
        {

            return View("Pa/AllCallRequests");
        }

        public async Task<ActionResult> PaAllCallRequestsData(GridParams gridparams)
        {
            Task<IQueryable<PaCallRequest>> task = _metroSupport.PaCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<PaCallRequest> callrequests = task.Result.AsQueryable();

            var gridbuilder = new GridModelBuilder<PaCallRequest>(callrequests.AsQueryable(), gridparams)
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
                    o.Comment
                }
            }.Build();

            return Json(gridbuilder);
        }

        public ActionResult PaAllCallRequestsByDate()
        {
            return View("Pa/AllCallRequestsByDate");
        }


        public async Task<ActionResult> PaAllCallRequestsByDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<PaCallRequest>> task = _metroSupport.PaCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<PaCallRequest> callrequests = task.Result.AsQueryable();


            return Json(BuildPaGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult PaAllCallRequestsByDateInProduce()
        {
            return View("Pa/AllCallRequestsByDateInProduce");
        }
        public async Task<ActionResult> PaAllCallRequestsByDateInProduceData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<PaCallRequest>> task = _metroSupport.PaCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<PaCallRequest> callrequests = task.Result.AsQueryable();

            return Json(BuildPaGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult PaAllCallRequestsByAssignerAndDate()
        {
            return View("Pa/AllCallRequestsByAssignerAndDate");
        }

        public async Task<ActionResult> PaAllCallRequestsByAssignerAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<PaCallRequest>> task = _metroSupport.PaCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<PaCallRequest> callrequests = task.Result.AsQueryable();


            return Json(BuildPaGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult PaAllCallRequestsByBossAndDate()
        {
            return View("Pa/AllCallRequestsByBossAndDate");
        }

        public async Task<ActionResult> PaAllCallRequestsByBossAndDateData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<PaCallRequest>> task = _metroSupport.PaCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<PaCallRequest> callrequests = task.Result.AsQueryable();


            return Json(BuildPaGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult PaAllCallRequestsByStatus()
        {
            return View("Pa/AllCallRequestsByStatus");
        }

        public async Task<ActionResult> PaAllCallRequestsByStatusData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<PaCallRequest>> task = _metroSupport.PaCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<PaCallRequest> callrequests = task.Result.AsQueryable();

            return Json(new GridModelBuilder<PaCallRequest>(callrequests, gridparams)
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
                    o.Comment
                }
            }.Build());
        }

        public ActionResult PaAllCallRequestsByRequestor()
        {
            return View("Pa/AllCallRequestsByRequestor");
        }
        public async Task<ActionResult> PaAllCallRequestsByRequestorData(GridParams gridparams, bool collapsed = true)
        {
            Task<IQueryable<PaCallRequest>> task = _metroSupport.PaCallRepository.GetAllRequestsAsync();
            await Task.WhenAll(task);
            IQueryable<PaCallRequest> callrequests = task.Result.AsQueryable();

            return Json(BuildPaGridParameters(callrequests, gridparams, collapsed));
        }

        public ActionResult NewPaCallRequest()
        {
            return RedirectToAction("Index", "PaCallRequest");
        }

        [HttpPost]
        public ActionResult PaSimpleSearch(string searchvalue)
        {
            ViewBag.SearchValue = searchvalue;
            return View("Pa/SimpleSearch");
        }

        public ActionResult PaSimpleSearchData(GridParams gridparams, string searchvalue)
        {

            if (String.IsNullOrWhiteSpace(searchvalue))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IQueryable<SearchResultModel> result = _metroSupport.MetroSearch.SimpleSearch(SearchEntry.Pa, searchvalue);

            var gridbuilder = new GridModelBuilder<SearchResultModel>(result, gridparams)
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
        public ActionResult PaAdvanceSearch(FilterViewModel filtermodel)
        {
            if (filtermodel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SearchResult_FilterViewModel FilterResultModel = new SearchResult_FilterViewModel
            {
                Filter = new FilterViewModel(),
                SearchResult = _metroSupport.MetroSearch.AdvaceSearch(SearchEntry.Pa, filtermodel).Take(200)
            };

            if (FilterResultModel.SearchResult == null)
            {
                return HttpNotFound();
            }
            return View("Pa/AdvanceSearch", FilterResultModel);
        }


        [HttpPost]
        public JsonResult DeletePaCallRequests(string selectedIds)
        {
            if (String.IsNullOrWhiteSpace(selectedIds))
            {
                return Json(false);
            }
            string[] requestIds = Regex.Split(selectedIds, ";");
            bool statement = _metroSupport.PaCallRepository.DeleteCallRequest(requestIds);
            if (!statement)
            {
                return Json(false);
            }
            return Json(true);
        }

        public FileResult PaExport(string ids)
        {

            return File("", "");
        }

        [HttpPost]
        public FileResult PaReport(FilterViewModel filtermodel)
        {
            if (filtermodel == null)
            {
                return File("", "");
            }
            List<SearchResultModel> result = _metroSupport.MetroSearch.AdvaceSearch(SearchEntry.Pa, filtermodel).ToList();
            return File(_metroSupport.DataExport.ExportTo(new MsExcel(), result.ListToDataTable()), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
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

          private GridModelDto<ItCallRequest> BuildItGridParameters(IQueryable<ItCallRequest> callrequests, GridParams g,
            bool collapsed)
        {

            return new GridModelBuilder<ItCallRequest>(callrequests, g)
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
                    o.Comment
                }
            }.Build();
        }

     private GridModelDto<AsppCallRequest> BuildAsppGridParameters(IQueryable<AsppCallRequest> callrequests, GridParams g,
            bool collapsed)
        {

            return new GridModelBuilder<AsppCallRequest>(callrequests, g)
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
                    o.Comment
                }
            }.Build();
        }

     private GridModelDto<SvyazCallRequest> BuildSvyazGridParameters(IQueryable<SvyazCallRequest> callrequests, GridParams g,
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
                 o.Comment
             }
         }.Build();
     }

     private GridModelDto<PaCallRequest> BuildPaGridParameters(IQueryable<PaCallRequest> callrequests, GridParams g,
         bool collapsed)
     {

         return new GridModelBuilder<PaCallRequest>(callrequests, g)
         {
             Key = "CallRequestId",
             MakeHeader = mh =>
             {
                    var firstitem = mh.Items.First();
                    var val = string.Join(" ", AweUtil.GetColumnValue(mh.Column, firstitem).Select(o=>o.ToDateTimeGridStr()));
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
                 o.Comment
             }
         }.Build();
     }

    }
}
