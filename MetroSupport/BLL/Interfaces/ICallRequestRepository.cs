using MetroSupport.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using MetroSupport.Models;

namespace MetroSupport.BLL.Interfaces
{
    public interface ICallRequestRepository<T>
    {
        IQueryable<T> GetAllRequests();
        Task<IQueryable<T>> GetAllRequestsAsync();
        IQueryable<T> GetAllRequestsByUser(IPrincipal user);
        T GetRequestById(Guid reqid);
        IQueryable<T> GetAllRequestForMonth(int month, IPrincipal user);
        IQueryable<T> GetAllOpenRequests();
        IQueryable<T> GetAllOpenRequestsByUser(IPrincipal user);
        IQueryable<T> GetAllOpenRequestsForMonth(int month, IPrincipal user);
        Task<IQueryable<T>> GetAllRequestsByUserAsync(IPrincipal user);
        Task<IQueryable<T>> GetAllRequestForMonthAsync(int month, IPrincipal user);
        Task<IQueryable<T>> GetAllOpenRequestsAsync();
        Task<IQueryable<T>> GetAllOpenRequestsByUserAsync(IPrincipal user);
        Task<IQueryable<T>> GetAllOpenRequestsForMonthAsync(int month, IPrincipal user);
        LogViewModel GetRequestLog(string reqid);
        T SaveCallRequest(T request, string user);
        T UpdateCallRequest(T request, string user);
        bool DeleteCallRequest(string[] requestsId);
        bool SetOnHoldRequest(string requestid, string description, string user);
        bool DenyCallRequest(string reqid, string description, string user);
        bool AcceptToWorkCallRequest(string reqid, string user);
        bool SendToAcceptCallRequest(string reqid, string user);
        bool CloseCallRequest(string reqid, string user);
        bool ReturnToAssignerCallRequest(string requestid, string description, string user);

    }
}