using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MetroSupport.Commons;
using MetroSupport.Models;
using MetroSupport.ViewModels;

namespace MetroSupport.BLL.Interfaces
{
    public interface IWidget
    {
        IQueryable<TopCallRequest> GetTopCallRequests(string username, Enums.Department department);
        RequestsCount GetCallRequestsCount(string username, Enums.Department department);
        Task<IQueryable<TopCallRequest>> GetTopCallRequestsAsync(string username, Enums.Department department);
        Task<RequestsCount> GetCallRequestsCountAsync(string username, Enums.Department department);
    }
}