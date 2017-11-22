using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MetroSupport.BLL.Interfaces;
using MetroSupport.Commons;
using MetroSupport.Models;
using MetroSupport.ViewModels;

namespace MetroSupport.BLL.Implements
{
    public class Widget:IWidget
    {
        private readonly MetroSupportContext _metro;

        public Widget(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public IQueryable<TopCallRequest> GetTopCallRequests(string username, Enums.Department department)
        {
            switch (department)
            {
               case  Enums.Department.It: return _metro.ItCallRequests.Where(x => x.Status == 5 && x.IsWorkingOn == 0 && (x.AssignTo == username || x.AssignBoss == username)).OrderByDescending(x=>x.Creation).Take(7).ToTopCallRequest();
               case Enums.Department.Aspp: return _metro.AsppCallRequests.Where(x => x.Status == 5 && x.IsWorkingOn == 0 && (x.AssignTo == username || x.AssignBoss == username)).OrderByDescending(x => x.Creation).Take(7).ToTopCallRequest();
               case Enums.Department.Svyaz: return _metro.SvyazCallRequests.Where(x => x.Status == 5 && x.IsWorkingOn == 0 && (x.AssignTo == username || x.AssignBoss == username)).OrderByDescending(x => x.Creation).Take(7).ToTopCallRequest();
               case Enums.Department.Pa: return _metro.PaCallRequests.Where(x => x.Status == 5 && x.IsWorkingOn == 0 && (x.AssignTo == username || x.AssignBoss == username)).OrderByDescending(x => x.Creation).Take(7).ToTopCallRequest();
               default: return null;
            }
        }

        public RequestsCount GetCallRequestsCount(string username, Enums.Department department)
        {
            RequestsCount  requestcount = new RequestsCount();
            if(department == Enums.Department.It)
            {
                requestcount.InWork = _metro.ItCallRequests.Count(x => x.Status == 5 && x.IsWorkingOn == 1 && (x.AssignTo == username || x.AssignBoss == username));
                requestcount.HoldOn = _metro.ItCallRequests.Count(x => x.Status == 4 && (x.AssignTo == username || x.AssignBoss == username));
                requestcount.Close = _metro.ItCallRequests.Count(x => x.Status == 12 && (x.AssignTo == username || x.AssignBoss == username));
                return requestcount;
            }
            if (department == Enums.Department.Aspp)
            {
                requestcount.InWork = _metro.AsppCallRequests.Count(x => x.Status == 5 && x.IsWorkingOn == 1 && (x.AssignTo == username || x.AssignBoss == username));
                requestcount.HoldOn = _metro.AsppCallRequests.Count(x => x.Status == 4 && (x.AssignTo == username || x.AssignBoss == username));
                requestcount.Close = _metro.AsppCallRequests.Count(x => x.Status == 12 && (x.AssignTo == username || x.AssignBoss == username));
                return requestcount;
            }
            if (department == Enums.Department.Svyaz)
            {
                requestcount.InWork = _metro.SvyazCallRequests.Count(x => x.Status == 5 && x.IsWorkingOn == 1 && (x.AssignTo == username || x.AssignBoss == username));
                requestcount.HoldOn = _metro.SvyazCallRequests.Count(x => x.Status == 4 && (x.AssignTo == username || x.AssignBoss == username));
                requestcount.Close = _metro.SvyazCallRequests.Count(x => x.Status == 12 && (x.AssignTo == username || x.AssignBoss == username));
                return requestcount;
            }
            if (department == Enums.Department.Pa)
            {
                requestcount.InWork = _metro.PaCallRequests.Count(x => x.Status == 5 && x.IsWorkingOn == 1 && (x.AssignTo == username || x.AssignBoss == username));
                requestcount.HoldOn = _metro.PaCallRequests.Count(x => x.Status == 4 && (x.AssignTo == username || x.AssignBoss == username));
                requestcount.Close = _metro.PaCallRequests.Count(x => x.Status == 12 && (x.AssignTo == username || x.AssignBoss == username));
                return requestcount;
            }
            return null;
        }

     
        public async Task<IQueryable<TopCallRequest>> GetTopCallRequestsAsync(string username, Enums.Department department)
        {
            return await Task.Factory.StartNew(()=>GetTopCallRequests(username,department));
        }

        public async Task<RequestsCount> GetCallRequestsCountAsync(string username, Enums.Department department)
        {
            return await Task.Factory.StartNew(() => GetCallRequestsCount(username, department));
        }
    }
}