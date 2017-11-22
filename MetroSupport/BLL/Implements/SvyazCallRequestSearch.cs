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
    public class SvyazCallRequestSearch : ICallRequestSearch<SvyazCallRequest>
    {
        private readonly MetroSupportContext _metro;
        public SvyazCallRequestSearch(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public IQueryable<SvyazCallRequest> SimpleSearch(string searchvalue)
        {
            return _metro.SvyazCallRequests.Where(x=>x.RequestNumber.Contains(searchvalue) || Status.StatusConvertion(x.Status,x.IsWorkingOn).Contains(searchvalue) ||
                    x.TroubleSubject.Contains(searchvalue) || x.RequestorName.Contains(searchvalue) || x.AssignTo.Contains(searchvalue) || x.AssignBoss.Contains(searchvalue) || x.TroubleReason.Contains(searchvalue));
        }

        public IQueryable<SvyazCallRequest> AdvanceSearch(FilterViewModel filtermodel)
        {
            IQueryable<SvyazCallRequest> requests = _metro.SvyazCallRequests.ToList().AsQueryable();
            CallRequestFilter<SvyazCallRequest> requestfilter = new MetroCallRequestFilter<SvyazCallRequest>(requests);
            if(!String.IsNullOrWhiteSpace(filtermodel.Assigner)) 
                requestfilter = new SetCallRequestFilterOption<SvyazCallRequest>(x => x.AssignTo == filtermodel.Assigner,requests,requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.Boss)) 
                requestfilter = new SetCallRequestFilterOption<SvyazCallRequest>(x => x.AssignBoss == filtermodel.Boss, requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.Requestor)) 
                requestfilter = new SetCallRequestFilterOption<SvyazCallRequest>(x => x.RequestorName == filtermodel.Requestor, requests, requestfilter);
              if (filtermodel.Status != 0)
                requestfilter = new SetCallRequestFilterOption<SvyazCallRequest>(x => x.Status == filtermodel.Status, requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.TroubleReason.ToString()))
                requestfilter = new SetCallRequestFilterOption<SvyazCallRequest>(x => x.TroubleReason == filtermodel.TroubleReason, requests, requestfilter);
            if (!String.IsNullOrWhiteSpace(filtermodel.TroubleTheme))
                requestfilter = new SetCallRequestFilterOption<SvyazCallRequest>(x => x.TroubleSubject == filtermodel.TroubleTheme, requests, requestfilter);
            if ((filtermodel.StartDate != null) && (filtermodel.EndDate != null))
                requestfilter = new SetCallRequestFilterOption<SvyazCallRequest>((x => x.Creation.Value.Date >= filtermodel.StartDate && x.Creation.Value.Date <= filtermodel.EndDate), requests, requestfilter);
            IQueryable<SvyazCallRequest> callrequests = requestfilter.FilterResult();

            return callrequests;
        }
    }
}