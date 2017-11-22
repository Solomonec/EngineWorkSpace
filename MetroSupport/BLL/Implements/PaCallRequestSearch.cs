using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using MetroSupport.BLL.Implements.Filter;
using MetroSupport.BLL.Interfaces;
using MetroSupport.BLL.Interfaces.Filter;
using MetroSupport.Commons;
using MetroSupport.Models;
using MetroSupport.ViewModels;

namespace MetroSupport.BLL.Implements
{
    public class PaCallRequestSearch : ICallRequestSearch<PaCallRequest>
    {
        private readonly MetroSupportContext _metro;
        public PaCallRequestSearch(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public IQueryable<PaCallRequest> SimpleSearch(string searchvalue)
        {
            return _metro.PaCallRequests.Where(x=>x.RequestNumber.Contains(searchvalue) || Status.StatusConvertion(x.Status,x.IsWorkingOn).Contains(searchvalue) ||
                    x.TroubleSubject.Contains(searchvalue) || x.RequestorName.Contains(searchvalue) || x.AssignTo.Contains(searchvalue) || x.AssignBoss.Contains(searchvalue) || x.TroubleReason.Contains(searchvalue)||
                    x.Prevention.Contains(searchvalue) || x.DeviceType.Contains(searchvalue));
        }

        public IQueryable<PaCallRequest> AdvanceSearch(FilterViewModel filtermodel)
        {
            IQueryable<PaCallRequest> requests = _metro.PaCallRequests.ToList().AsQueryable();
            CallRequestFilter<PaCallRequest> requestfilter = new MetroCallRequestFilter<PaCallRequest>(requests);
            if(!String.IsNullOrWhiteSpace(filtermodel.Assigner)) 
                requestfilter = new SetCallRequestFilterOption<PaCallRequest>(x => x.AssignTo == filtermodel.Assigner,requests,requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.Boss)) 
                requestfilter = new SetCallRequestFilterOption<PaCallRequest>(x => x.AssignBoss == filtermodel.Boss, requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.Requestor)) 
                requestfilter = new SetCallRequestFilterOption<PaCallRequest>(x => x.RequestorName == filtermodel.Requestor, requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.DeviceName)) 
                requestfilter = new SetCallRequestFilterOption<PaCallRequest>(x => x.DeviceName == filtermodel.DeviceName, requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.DeviceModel)) 
                requestfilter = new SetCallRequestFilterOption<PaCallRequest>(x => x.DeviceModel == filtermodel.DeviceModel, requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.Prevention)) 
                requestfilter = new SetCallRequestFilterOption<PaCallRequest>(x => x.Prevention == filtermodel.Prevention, requests, requestfilter);
            if (filtermodel.Status != 0)
                requestfilter = new SetCallRequestFilterOption<PaCallRequest>(x => x.Status == filtermodel.Status, requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.TroubleReason))
                requestfilter = new SetCallRequestFilterOption<PaCallRequest>(x => x.TroubleReason == filtermodel.TroubleReason, requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.TroubleTheme))
                requestfilter = new SetCallRequestFilterOption<PaCallRequest>(x => x.TroubleSubject == filtermodel.TroubleTheme, requests, requestfilter);
            if ((filtermodel.StartDate != null) && (filtermodel.EndDate != null))
                requestfilter = new SetCallRequestFilterOption<PaCallRequest>((x => x.Creation.Value.Date >= filtermodel.StartDate && x.Creation.Value.Date <= filtermodel.EndDate), requests, requestfilter);
            IQueryable<PaCallRequest> callrequests = requestfilter.FilterResult();

            return callrequests;
        }
    }
}