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
    public class ItCallRequestSearch : ICallRequestSearch<ItCallRequest>
    {
        private readonly MetroSupportContext _metro;
        public ItCallRequestSearch(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public IQueryable<ItCallRequest> SimpleSearch(string searchvalue)
        {
            return _metro.ItCallRequests.Where(x=>x.RequestNumber.Contains(searchvalue) || 
                    x.TroubleSubject.Contains(searchvalue) || x.RequestorName.Contains(searchvalue) || x.AssignTo.Contains(searchvalue) || x.AssignBoss.Contains(searchvalue) || x.TroubleReason.Contains(searchvalue)||
                    x.Prevention.Contains(searchvalue) || x.DeviceType.Contains(searchvalue));
        }

        public IQueryable<ItCallRequest> AdvanceSearch(FilterViewModel filtermodel)
        {
            IQueryable<ItCallRequest> requests = _metro.ItCallRequests.ToList().AsQueryable();
            CallRequestFilter<ItCallRequest> requestfilter = new MetroCallRequestFilter<ItCallRequest>(requests);
            if(!String.IsNullOrWhiteSpace(filtermodel.Assigner))
                requestfilter = new SetCallRequestFilterOption<ItCallRequest>(x => x.AssignTo.Contains(filtermodel.Assigner), requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.Boss))
                requestfilter = new SetCallRequestFilterOption<ItCallRequest>(x => x.AssignBoss.Contains(filtermodel.Boss), requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.Requestor))
                requestfilter = new SetCallRequestFilterOption<ItCallRequest>(x => x.RequestorName.Contains(filtermodel.Requestor), requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.DeviceName))
                requestfilter = new SetCallRequestFilterOption<ItCallRequest>(x => x.DeviceName.Contains(filtermodel.DeviceName), requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.DeviceModel))
                requestfilter = new SetCallRequestFilterOption<ItCallRequest>(x => x.DeviceModel.Contains(filtermodel.DeviceModel), requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.Prevention))
                requestfilter = new SetCallRequestFilterOption<ItCallRequest>(x => x.Prevention == filtermodel.Prevention, requests, requestfilter);
            if (filtermodel.Status != 0)
                requestfilter = new SetCallRequestFilterOption<ItCallRequest>(x => x.Status == filtermodel.Status, requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.TroubleReason))
                requestfilter = new SetCallRequestFilterOption<ItCallRequest>(x => x.TroubleReason == filtermodel.TroubleReason, requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.TroubleTheme))
                requestfilter = new SetCallRequestFilterOption<ItCallRequest>(x => x.TroubleSubject.Contains(filtermodel.TroubleTheme), requests, requestfilter);
            if ((filtermodel.StartDate != null) && (filtermodel.EndDate != null))
                requestfilter = new SetCallRequestFilterOption<ItCallRequest>((x => x.Creation.Value.Date >= filtermodel.StartDate && x.Creation.Value.Date <= filtermodel.EndDate), requests, requestfilter);
            IQueryable<ItCallRequest> callrequests = requestfilter.FilterResult();

            return callrequests;
        }
    }
}