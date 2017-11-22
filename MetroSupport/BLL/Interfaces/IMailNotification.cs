using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using MetroSupport.Models;
using MetroSupport.ViewModels;
namespace MetroSupport.BLL.Interfaces
{
    public interface IMailNotification
    {
        MailData GetMailData(Guid callrequestid, string requestnumber, string troublesubject, UserProfile assigner,
            UserProfile boss, string troubledepartment); 
       void SendNotificationMessage(MailData credentials);

    }
}