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
    public class AsppCallRequestSearch : ICallRequestSearch<AsppCallRequest>
    {
        private readonly MetroSupportContext _metro;
        public AsppCallRequestSearch(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public IQueryable<AsppCallRequest> SimpleSearch(string searchvalue)
        {
            return _metro.AsppCallRequests.Where(x=>x.RequestNumber.Contains(searchvalue) || Status.StatusConvertion(x.Status,x.IsWorkingOn).Contains(searchvalue) ||
                    x.TroubleSubject.Contains(searchvalue) || x.RequestorName.Contains(searchvalue) || x.AssignTo.Contains(searchvalue) || x.AssignBoss.Contains(searchvalue) || x.TroubleReason.Contains(searchvalue)||
                    x.Prevention.Contains(searchvalue) || x.DeviceType.Contains(searchvalue));
        }

        public IQueryable<AsppCallRequest> AdvanceSearch(FilterViewModel filtermodel)
        {
            IQueryable<AsppCallRequest> requests = _metro.AsppCallRequests.ToList().AsQueryable();
            CallRequestFilter<AsppCallRequest> requestfilter = new MetroCallRequestFilter<AsppCallRequest>(requests);
            if(!String.IsNullOrWhiteSpace(filtermodel.Assigner)) 
                requestfilter = new SetCallRequestFilterOption<AsppCallRequest>(x => x.AssignTo == filtermodel.Assigner,requests,requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.Boss))
                requestfilter = new SetCallRequestFilterOption<AsppCallRequest>(x => x.AssignBoss == filtermodel.Boss, requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.Requestor))
                requestfilter = new SetCallRequestFilterOption<AsppCallRequest>(x => x.RequestorName == filtermodel.Requestor, requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.DeviceName))
                requestfilter = new SetCallRequestFilterOption<AsppCallRequest>(x => x.DeviceName == filtermodel.DeviceName, requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.DeviceModel))
                requestfilter = new SetCallRequestFilterOption<AsppCallRequest>(x => x.DeviceModel == filtermodel.DeviceModel, requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.Prevention))
                requestfilter = new SetCallRequestFilterOption<AsppCallRequest>(x => x.Prevention == filtermodel.Prevention, requests, requestfilter);
            if (filtermodel.Status != 0)
                requestfilter = new SetCallRequestFilterOption<AsppCallRequest>(x => x.Status == filtermodel.Status, requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.TroubleReason))
                requestfilter = new SetCallRequestFilterOption<AsppCallRequest>(x => x.TroubleReason == filtermodel.TroubleReason, requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.TroubleTheme))
                requestfilter = new SetCallRequestFilterOption<AsppCallRequest>(x => x.TroubleSubject == filtermodel.TroubleTheme, requests, requestfilter);
            if ((filtermodel.StartDate != null) && (filtermodel.EndDate != null))
                requestfilter = new SetCallRequestFilterOption<AsppCallRequest>((x => x.Creation.Value.Date >= filtermodel.StartDate && x.Creation.Value.Date <= filtermodel.EndDate), requests, requestfilter);
            IQueryable<AsppCallRequest> callrequests = requestfilter.FilterResult();

            return callrequests;
        }
    }
}