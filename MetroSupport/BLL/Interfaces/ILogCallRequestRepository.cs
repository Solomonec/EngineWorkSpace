using MetroSupport.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroSupport.Models;

namespace MetroSupport.BLL.Interfaces
{
    public interface ILogCallRequestRepository<T>
    {
        T GetCallRequestLogById(string reqid);
        T CreateCallRequestLog(Guid log, string user);
        T ChangeCallRequestLog(Guid log, string user);
        T SetOnHoldCallRequestLog(Guid logid, string user);
        T DenyCallRequestLog(Guid logid, string user);
        T AcceptToWorkCallRequestLog(Guid logid, string user);
        T ReturnCallRequestLog(Guid logid, string user);
        T CloseCallRequestLog(Guid logid, string user);
        T SendToApproveCallRequestLog(Guid logid, string user);

        LogViewModel RequestLogToViewModel(T log);


    }
}