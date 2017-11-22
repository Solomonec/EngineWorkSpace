using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroSupport.BLL.Interfaces;
using System.Security.Principal;
using MetroSupport.ViewModels;
using System.Data.Entity;
using System.Threading.Tasks;
using MetroSupport.Models;
using Ninject;

namespace MetroSupport.BLL.Implements
{
    public class SvyazCallRequestRepository : ICallRequestRepository<SvyazCallRequest>
        
    {
        private readonly MetroSupportContext _metro;


        private readonly ILogCallRequestRepository<SvyazCallRequestLog> _svyazrequestlog;
        public SvyazCallRequestRepository(MetroSupportContext metro)
        {
            _metro = metro;  
            _svyazrequestlog = new SvyazCallRequestLogRepository(metro);
        }

        
        public IQueryable<SvyazCallRequest> GetAllRequests()
        {
            return _metro.SvyazCallRequests;
        }

        public IQueryable<SvyazCallRequest> GetAllRequestsByUser(IPrincipal user)
        {
            return _metro.SvyazCallRequests.Where(x => x.AssignTo == user.Identity.Name);
        }

        public SvyazCallRequest GetRequestById(Guid reqid)
        {
            return _metro.SvyazCallRequests.FirstOrDefault(x => x.CallRequestId == reqid);
        }

        public IQueryable<SvyazCallRequest> GetAllRequestForMonth(int month, IPrincipal user)
        {
            return _metro.SvyazCallRequests.Where(x => x.Creation.Value.Month == month && x.AssignTo == user.Identity.Name);
        }

        public IQueryable<SvyazCallRequest> GetAllOpenRequests()
        {
            return _metro.SvyazCallRequests.Where(x => x.Status == 5);
        }

        public IQueryable<SvyazCallRequest> GetAllOpenRequestsByUser(IPrincipal user)
        {
            return _metro.SvyazCallRequests.Where(x => x.Status == 5 && x.AssignTo == user.Identity.Name);
        }

        public IQueryable<SvyazCallRequest> GetAllOpenRequestsForMonth(int month, IPrincipal user)
        {
            return _metro.SvyazCallRequests.Where(x => x.Status == 5 && x.Creation.Value.Month == month && x.AssignTo == user.Identity.Name);
        
        }

        public async Task<IQueryable<SvyazCallRequest>> GetAllRequestsByUserAsync(IPrincipal user)
        {
            return await Task.Factory.StartNew(() => GetAllRequestsByUser(user));
        }

        public async Task<IQueryable<SvyazCallRequest>> GetAllRequestForMonthAsync(int month, IPrincipal user)
        {
            return await Task.Factory.StartNew(() => GetAllRequestForMonth(month, user));
        }

        public async Task<IQueryable<SvyazCallRequest>> GetAllRequestsAsync()
        {
            return await Task.Factory.StartNew(() => GetAllRequests());
        }

        public async Task<IQueryable<SvyazCallRequest>> GetAllOpenRequestsAsync()
        {
            return await Task.Factory.StartNew<IQueryable<SvyazCallRequest>>(GetAllOpenRequests);
        }

        public async Task<IQueryable<SvyazCallRequest>> GetAllOpenRequestsByUserAsync(IPrincipal user)
        {
            return await Task.Factory.StartNew(() => GetAllOpenRequestsByUser(user));
        }

        public async Task<IQueryable<SvyazCallRequest>> GetAllOpenRequestsForMonthAsync(int month, IPrincipal user)
        {
            return await Task.Factory.StartNew(() => GetAllOpenRequestsForMonth(month, user));
        }

        public LogViewModel GetRequestLog(string reqid)
        {
            return _svyazrequestlog.RequestLogToViewModel(_svyazrequestlog.GetCallRequestLogById(reqid));
        }

        public SvyazCallRequest SaveCallRequest(SvyazCallRequest request, string user)
        {
            if (request != null && request.Status == 0)
            {
                request.RequestNumber = (Convert.ToInt32(_metro.SvyazCallRequests.Max(x => x.RequestNumber)) + 1).ToString(); ;
                request.Creation = DateTime.Now.Date;
                request.Time = DateTime.Now;
                request.Status = 5;
                request.IsWorkingOn = 0;
                request.SvyazCallRequestLog = _svyazrequestlog.CreateCallRequestLog(request.CallRequestId, user);
                _metro.SvyazCallRequests.Add(request);
                _metro.SaveChanges();
                

                return request;
            }
            else
                return null;
        }

        public SvyazCallRequest UpdateCallRequest(SvyazCallRequest request, string user)
        {
            SvyazCallRequest currentrequest = _metro.SvyazCallRequests.FirstOrDefault(x => x.CallRequestId == request.CallRequestId);
            if (currentrequest != null)
            {
                currentrequest.TroubleSubject = request.TroubleSubject;
                currentrequest.Comment = request.Comment;
                currentrequest.Category = request.Category;
                currentrequest.NextSubCategoryId = request.NextSubCategoryId;
                currentrequest.SubCategory1 = request.SubCategory1;
                currentrequest.NextSubCategoryId1 = request.NextSubCategoryId1;
                currentrequest.SubCategory2 = request.SubCategory2;
                currentrequest.NextSubCategoryId2 = request.NextSubCategoryId2;
                currentrequest.SubCategory3 = request.SubCategory3;
                currentrequest.NextSubCategoryId3 = request.NextSubCategoryId3;
                currentrequest.SubCategory4 = request.SubCategory4;
                currentrequest.NextSubCategoryId4 = request.NextSubCategoryId4;
                currentrequest.SubCategory5 = request.SubCategory5;
                currentrequest.NextSubCategoryId5 = request.NextSubCategoryId5;
                currentrequest.Model = request.Model;
                currentrequest.ModelId = request.ModelId;
                currentrequest.RequestorName = request.RequestorName;
                currentrequest.Organization = request.Organization;
                currentrequest.Department = request.Department;
                currentrequest.Tel = request.Tel;
                currentrequest.Address = request.Address;
                currentrequest.Room = request.Room;
                currentrequest.Floor = request.Floor;
                currentrequest.Location = request.Location;
                currentrequest.AssignTo = request.AssignTo;
                currentrequest.AssignBoss = request.AssignBoss;
                currentrequest.AssignDepartment = request.AssignDepartment;
                currentrequest.StartDateInWork = request.StartDateInWork;
                currentrequest.StartTimeInWork = request.StartTimeInWork;
                currentrequest.EndDateInWork = request.EndDateInWork;
                currentrequest.EndTimeInWork = request.EndTimeInWork;
                currentrequest.TotalWorkInDays = request.TotalWorkInDays;
                currentrequest.TotalWorkInHours = request.TotalWorkInHours;
                currentrequest.TotalWorkInMinutes = request.TotalWorkInMinutes;
                currentrequest.TroubleReason = request.TroubleReason;
                currentrequest.TroubleDescription = request.TroubleDescription;
                currentrequest.ResolveDescription = request.ResolveDescription;
                    
               _metro.Entry(request).State = EntityState.Modified;
               _metro.SaveChanges();
               _svyazrequestlog.ChangeCallRequestLog(request.CallRequestId, user);
                return currentrequest;
            }
            else return null;
        }

        public bool DeleteCallRequest(string[] requestsId)
        {
            foreach (string id in requestsId)
            {
                Guid guidid = Guid.Parse(id);
                SvyazCallRequest request = _metro.SvyazCallRequests.Include("SvyazCallRequestLog").FirstOrDefault(x => x.CallRequestId == guidid);
                if (request != null)
                {
                    _metro.SvyazCallRequestLogs.Remove(request.SvyazCallRequestLog);
                    _metro.SvyazCallRequests.Remove(request);
                    _metro.SaveChanges();

                }
                else
                {
                    return false;
                }
            }
            return true;
            

        }

        public bool SetOnHoldRequest(string requestid, string description, string user)
        {
            throw new NotImplementedException();
        }

        public bool DenyCallRequest(string reqid, string description, string user)
        {
            throw new NotImplementedException();
        }

        public bool AcceptToWorkCallRequest(string reqid, string user)
        {
            throw new NotImplementedException();
        }

        public bool SendToAcceptCallRequest(string reqid, string user)
        {
            throw new NotImplementedException();
        }

        public bool CloseCallRequest(string reqid, string user)
        {
            throw new NotImplementedException();
        }

        public bool ReturnToAssignerCallRequest(string requestid, string description, string user)
        {
            throw new NotImplementedException();
        }

        public bool SetOnHoldRequest(string requestid, string description)
        {
            Guid guidid = Guid.Parse(requestid);
            SvyazCallRequest request = _metro.SvyazCallRequests.FirstOrDefault(x => x.CallRequestId == guidid);
            if (request != null && (request.Status == 5 || request.Status == 4) && request.IsWorkingOn == 1)
            {
                request.Status = 4;
                request.ResolveDescription += "\n********** " + DateTime.Now + " ***** Продлена ***** \n" + "********************************************* \n" + description;
               _metro.Entry(request).State = EntityState.Modified;
                _metro.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool DenyCallRequest(string requestid, string description)
        {
            Guid guidid = Guid.Parse(requestid);
            SvyazCallRequest request = _metro.SvyazCallRequests.FirstOrDefault(x => x.CallRequestId == guidid);
            if (request != null && (request.Status != -1 || request.Status != 12 || request.Status != 9))
            {
                request.Status = -1;
                request.ResolveDescription += "\n********** " + DateTime.Now + " ***** Отклонена ***** \n" + "************************************** \n" + description;
                _metro.Entry(request).State = EntityState.Modified;
                _metro.SaveChanges();
                return true;
            }
            else               
            return false;
        }

        public bool AcceptToWorkCallRequest(string reqid)
        {
            if (reqid != String.Empty )
            {
                Guid guidid = Guid.Parse(reqid);
                var request = _metro.SvyazCallRequests.FirstOrDefault(x => x.CallRequestId == guidid);
                if (request != null && request.Status == 5 && request.IsWorkingOn == 0)
                {
                    request.StartDateInWork = DateTime.Now;
                    request.StartTimeInWork = DateTime.Now;
                    request.IsWorkingOn = 1;
                    _metro.Entry(request).State = EntityState.Modified;
                    _metro.SaveChanges();
                    return true;
                }
                else return false;
            }
            else
                return false;
        }

        public bool SendToAcceptCallRequest(string reqid)
        {
            if (reqid != String.Empty)
            {
                Guid guidid = Guid.Parse(reqid);
                var request = _metro.SvyazCallRequests.FirstOrDefault(x => x.CallRequestId == guidid);
                if (request != null && (request.Status == 5 && request.Status == 4) && request.IsWorkingOn == 1 && request.Status != 12)
                {
                    request.Status = 9;
                    _metro.Entry(request).State = EntityState.Modified;
                    _metro.SaveChanges();
                    return true;
                }
                else return false;
            }
            else
                return false;
        }

        public bool CloseCallRequest(string reqid)
        {
            if (reqid != String.Empty)
            {
                Guid guidid = Guid.Parse(reqid);
                var request = _metro.SvyazCallRequests.FirstOrDefault(x => x.CallRequestId == guidid);
                if (request != null && request.Status == 9)
                {
                    request.Status = 12;
                    _metro.Entry(request).State = EntityState.Modified;
                    _metro.SaveChanges();
                    return true;
                }
                else return false;
            }
            else
                return false;
        }

        public bool ReturnToAssignerCallRequest(string requestid, string description)
        {
            if (requestid != String.Empty)
            {
                Guid guidid = Guid.Parse(requestid);
                var request = _metro.SvyazCallRequests.FirstOrDefault(x => x.CallRequestId == guidid);
                if (request != null && request.Status == 9 && request.Status != 12)
                {
                    request.Status = 5;
                    request.ResolveDescription += "\n********** " + DateTime.Now + " ***** Возвращена на выполнение ***** \n" + "************************************** \n" + description;
                    _metro.Entry(request).State = EntityState.Modified;
                    _metro.SaveChanges();
                    return true;
                }
                else return false;
            }
            else
                return false;
        }


      
    }
}