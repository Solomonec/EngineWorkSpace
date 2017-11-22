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
    public class AsppCallRequestRepository : ICallRequestRepository<AsppCallRequest>
        
    {
        private readonly MetroSupportContext _metro;


        private readonly ILogCallRequestRepository<AsppCallRequestLog> _aspprequestlog;

        public AsppCallRequestRepository(MetroSupportContext metro)
        {
            _metro = metro;  
            _aspprequestlog = new AsppCallRequestLogRepository(metro);
        }


        public IQueryable<AsppCallRequest> GetAllRequests()
        {
            return _metro.AsppCallRequests;
        }

        public async Task<IQueryable<AsppCallRequest>> GetAllRequestsAsync()
        {
            return await Task.Factory.StartNew(() => GetAllRequests());
        }

        public IQueryable<AsppCallRequest> GetAllRequestsByUser(IPrincipal user)
        {
           return _metro.AsppCallRequests.Where(x => x.AssignTo == user.Identity.Name);
        }

        public AsppCallRequest GetRequestById(Guid reqid)
        {
            return _metro.AsppCallRequests.FirstOrDefault(x => x.CallRequestId == reqid);
        }

        public IQueryable<AsppCallRequest> GetAllRequestForMonth(int month, IPrincipal user)
        {
            return _metro.AsppCallRequests.Where(x => x.Creation.Value.Month == month && x.AssignTo == user.Identity.Name);
        }

        public IQueryable<AsppCallRequest> GetAllOpenRequests()
        {
            return _metro.AsppCallRequests.Where(x => x.Status == 5);
        }

        public IQueryable<AsppCallRequest> GetAllOpenRequestsByUser(IPrincipal user)
        {
            return _metro.AsppCallRequests.Where(x => x.Status == 5 && x.AssignTo == user.Identity.Name);
        }


        public IQueryable<AsppCallRequest> GetAllOpenRequestsForMonth(int month, IPrincipal user)
        {
            return _metro.AsppCallRequests.Where(x => x.Status == 5 && x.Creation.Value.Month == month && x.AssignTo == user.Identity.Name);
        
        }

        public async Task<IQueryable<AsppCallRequest>> GetAllRequestsByUserAsync(IPrincipal user)
        {
            return await Task.Factory.StartNew(() => GetAllRequestsByUser(user));
        }

        public async Task<IQueryable<AsppCallRequest>> GetAllRequestForMonthAsync(int month, IPrincipal user)
        {
            return await Task.Factory.StartNew(() => GetAllRequestForMonth(month, user));
        }

        public async Task<IQueryable<AsppCallRequest>> GetAllOpenRequestsAsync()
        {
            return await Task.Factory.StartNew<IQueryable<AsppCallRequest>>(GetAllOpenRequests);
        }

        public async Task<IQueryable<AsppCallRequest>> GetAllOpenRequestsByUserAsync(IPrincipal user)
        {
            return await Task.Factory.StartNew(() => GetAllOpenRequestsByUser(user));
        }

        public async Task<IQueryable<AsppCallRequest>> GetAllOpenRequestsForMonthAsync(int month, IPrincipal user)
        {
            return await Task.Factory.StartNew(() => GetAllOpenRequestsForMonth(month, user));
        }

        public LogViewModel GetRequestLog(string reqid)
        {
            return _aspprequestlog.RequestLogToViewModel(_aspprequestlog.GetCallRequestLogById(reqid));
        }

        public AsppCallRequest SaveCallRequest(AsppCallRequest request, string user)
        {
            if (request != null && request.Status == 0)
            {
                request.RequestNumber = (Convert.ToInt32(_metro.AsppCallRequests.Max(x => x.RequestNumber)) + 1).ToString(); ;
                request.Creation = DateTime.Now.Date;
                request.Time = DateTime.Now;
                request.Status = 5;
                request.IsWorkingOn = 0;
                request.AsppCallRequestLog = _aspprequestlog.CreateCallRequestLog(request.CallRequestId, user);
                _metro.AsppCallRequests.Add(request);
                _metro.SaveChanges();
                
               return request;
            }
            else
                return null;
        }

        public AsppCallRequest UpdateCallRequest(AsppCallRequest request, string user)
        {
            if (request != null && request.Status != -1)
            {


                AsppCallRequest currentrequest = _metro.AsppCallRequests.FirstOrDefault(x => x.CallRequestId == request.CallRequestId);
                if (currentrequest != null)
                {
                    currentrequest.TroubleSubject = request.TroubleSubject;
                    currentrequest.Comment = request.Comment;
                    currentrequest.InvNumber = request.InvNumber;
                    currentrequest.DeviceName = request.DeviceName;
                    currentrequest.DeviceClass = request.DeviceClass;
                    currentrequest.DeviceModel = request.DeviceModel;
                    currentrequest.DateInWork = request.DateInWork;
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
                    currentrequest.Prevention = request.Prevention;
                    currentrequest.Remote = request.Remote;
                    currentrequest.TroubleDescription = request.TroubleDescription;
                    currentrequest.ResolveDescription = request.ResolveDescription;

                    currentrequest.AsppCallRequestLog = _aspprequestlog.ChangeCallRequestLog(request.CallRequestId, user);
                    _metro.Entry(currentrequest).State = EntityState.Modified;
                   
                    _metro.SaveChanges();

                    return currentrequest;
                }
                return null;

            }
            else return null;
        }

        public bool DeleteCallRequest(string[] requestsId)
        {
            foreach (string id in requestsId)
            {
                Guid guidid = Guid.Parse(id);
                AsppCallRequest request = _metro.AsppCallRequests.Include("AsppCallRequestLog").FirstOrDefault(x => x.CallRequestId == guidid);
                if (request != null)
                {
                    _metro.AsppCallRequestLogs.Remove(request.AsppCallRequestLog);
                    _metro.AsppCallRequests.Remove(request);
                    _metro.SaveChanges();

                }
                else
                {
                    return false;
                }
            }
            return true;
            

        }

        public bool SetOnHoldRequest(string requestid,string description, string user)
        {
            Guid guidid = Guid.Parse(requestid);
            AsppCallRequest request = _metro.AsppCallRequests.FirstOrDefault(x => x.CallRequestId == guidid);
            if (request != null && (request.Status == 5 || request.Status == 4) && request.IsWorkingOn == 1)
            {
                request.Status = 4;
                request.ResolveDescription += "\n********** " + DateTime.Now + " ***** Продлена ***** \n" + "************************************** \n" + description;
                request.AsppCallRequestLog = _aspprequestlog.SetOnHoldCallRequestLog(request.CallRequestId, user);
                _metro.Entry(request).State = EntityState.Modified;
              
               _metro.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool DenyCallRequest(string requestid, string description, string user)
        {
            Guid guidid = Guid.Parse(requestid);
            AsppCallRequest request = _metro.AsppCallRequests.FirstOrDefault(x => x.CallRequestId == guidid);
            if (request != null && (request.Status != -1 || request.Status != 12 || request.Status != 9))
            {
                request.Status = -1;
                request.ResolveDescription += "\n********** " + DateTime.Now + " ***** Отклонена ***** \n" + "************************************** \n" + description;
                request.AsppCallRequestLog = _aspprequestlog.DenyCallRequestLog(request.CallRequestId, user);
                _metro.Entry(request).State = EntityState.Modified;
                _metro.SaveChanges();
                return true;
            }
            else               
            return false;
        }

        public bool AcceptToWorkCallRequest(string reqid, string user)
        {
            if (reqid != String.Empty )
            {
                Guid guidid = Guid.Parse(reqid);
                var request = _metro.AsppCallRequests.FirstOrDefault(x => x.CallRequestId == guidid);
                if (request != null && request.Status == 5 && request.IsWorkingOn == 0)
                {
                    request.StartDateInWork = DateTime.Now;
                    request.StartTimeInWork = DateTime.Now;
                    request.IsWorkingOn = 1;
                      request.AsppCallRequestLog = _aspprequestlog.AcceptToWorkCallRequestLog(request.CallRequestId, user);
                    _metro.Entry(request).State = EntityState.Modified;
                    _metro.SaveChanges();
                    return true;
                }
                else return false;
            }
            else
                return false;
        }

        public bool SendToAcceptCallRequest(string reqid, string user)
        {
            if (reqid != String.Empty)
            {
                Guid guidid = Guid.Parse(reqid);
                var request = _metro.AsppCallRequests.FirstOrDefault(x => x.CallRequestId == guidid);
                if (request != null && (request.Status == 5 || request.Status == 4)  && request.IsWorkingOn == 1)
                {
                    request.Status = 9;
                    request.AsppCallRequestLog = _aspprequestlog.SendToApproveCallRequestLog(request.CallRequestId, user);
                    _metro.Entry(request).State = EntityState.Modified;
                    _metro.SaveChanges();
                    return true;
                }
                else return false;
            }
            else
                return false;
        }

        public bool CloseCallRequest(string reqid, string user)
        {
            if (reqid != String.Empty)
            {
                Guid guidid = Guid.Parse(reqid);
                var request = _metro.AsppCallRequests.FirstOrDefault(x => x.CallRequestId == guidid);
                if (request != null && request.Status == 9)
                {
                    request.Status = 12;
                    request.AsppCallRequestLog = _aspprequestlog.CloseCallRequestLog(request.CallRequestId, user);
                    _metro.Entry(request).State = EntityState.Modified;
                    _metro.SaveChanges();
                    return true;
                }
                else return false;
            }
            else
                return false;
        }

        public bool ReturnToAssignerCallRequest(string requestid, string description, string user)
        {
            if (requestid != String.Empty)
            {
                Guid guidid = Guid.Parse(requestid);
                var request = _metro.AsppCallRequests.FirstOrDefault(x => x.CallRequestId == guidid);
                if (request != null && request.Status == 9 && request.Status != 12)
                {
                    request.Status = 5;
                    request.ResolveDescription += "\n********** " + DateTime.Now + " ***** Возвращена на выполнение ***** \n" + "***************************************** \n" + description;
                    request.AsppCallRequestLog = _aspprequestlog.ReturnCallRequestLog(request.CallRequestId, user);
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