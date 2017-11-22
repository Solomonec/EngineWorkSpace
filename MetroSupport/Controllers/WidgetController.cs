using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MetroSupport.BLL.Services;
using MetroSupport.Commons;
using MetroSupport.Models;
using MetroSupport.ViewModels;

namespace MetroSupport.Controllers
{
    public class WidgetController : Controller
    {
          private readonly MetroSupportService _metroSupport;

          public WidgetController(MetroSupportService metroSupport)
        {
            _metroSupport = metroSupport;
        }

        [HttpPost]
        public async Task<JsonResult> RequestsCounts()
        {
            UserProfile user = _metroSupport.UserRepository.GetUserByName(User.Identity.Name);
            Task<RequestsCount> callrequestscounttask = _metroSupport.Widget.GetCallRequestsCountAsync(user.FullName,user.Department.ToEnumDepartment());
            await Task.WhenAll(callrequestscounttask);
            RequestsCount requestscount = new RequestsCount
            {
                InWork = callrequestscounttask.Result.InWork,
                HoldOn = callrequestscounttask.Result.HoldOn,
                Close = callrequestscounttask.Result.Close
            };

            return Json(requestscount);
        }
        
        [HttpPost]
        public async Task<JsonResult> TopCallRequests()
        {
            UserProfile user = _metroSupport.UserRepository.GetUserByName(User.Identity.Name);
            Task<IQueryable<TopCallRequest>> topcallrequeststask = _metroSupport.Widget.GetTopCallRequestsAsync(user.FullName, user.Department.ToEnumDepartment());
            await Task.WhenAll(topcallrequeststask);
            return Json(topcallrequeststask.Result.ToList());
            
        }


       
    }
}
