using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using MetroSupport.BLL.Interfaces;
using MetroSupport.Models;
using MetroSupport.ViewModels;

namespace MetroSupport.BLL.Implements
{
    public class ItCallRequestLogRepository: ILogCallRequestRepository<ItCallRequestLog>
    {
        private readonly MetroSupportContext _metro;

        public ItCallRequestLogRepository(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public ItCallRequestLog SendToApproveCallRequestLog(Guid logid, string user)
        {
            var log = _metro.ItCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now + " Пользователь " + user + " отправил заявку на одобрение;";
            return log;
        
        }

        public LogViewModel RequestLogToViewModel(ItCallRequestLog log)
        {
            if (log != null)
            {
                LogViewModel lvm = new LogViewModel
                {
                    CreateDate = log.CreationDate,
                    WhoCreate = log.Creator,
                    WhoChange = log.WhoLastUpdate,
                    ChangeDate = log.LastUpdateDate,
                    LogTextCollection = ParseCallRequestLogRecords(log.LogText)
                };
                return lvm;
            }
            return null;
        }

        public ItCallRequestLog GetCallRequestLogById(string reqid)
        {
            Guid guidid = Guid.Parse(reqid);
            if (reqid != String.Empty)
            {
                ItCallRequestLog log = _metro.ItCallRequestLogs.FirstOrDefault(x => x.LogId == guidid);
                return log;
            }
            return null;
        }

        public ItCallRequestLog CreateCallRequestLog(Guid id, string user)
        {
            ItCallRequestLog nlog = new ItCallRequestLog
            {
                LogId = id,
                CreationDate = DateTime.Now,
                Creator = user,
                LastUpdateDate = DateTime.Now,
                WhoLastUpdate = user,
                LogText = DateTime.Now.ToString() + " Пользователь " + user + " создал заявку;"
            };

            return nlog;
        }

        public ItCallRequestLog ChangeCallRequestLog(Guid logid, string user)
        {
            var log = _metro.ItCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now.ToString() + " Пользователь " + user + " внес изменения в информацию о заявке;";
            return log;
        }

        public ItCallRequestLog SetOnHoldCallRequestLog(Guid logid, string user)
        {
            var log = _metro.ItCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now + " Пользователь " + user + " приостановил выполнение заявки;";
            return log;
        }

        public ItCallRequestLog DenyCallRequestLog(Guid logid, string user)
        {
            var log = _metro.ItCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now + " Пользователь " + user + " отклонил заявку;";
            return log;
        }

        public ItCallRequestLog AcceptToWorkCallRequestLog(Guid logid, string user)
        {
            var log = _metro.ItCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now + " Пользователь " + user + " принял заявку в работу;";
            return log;
        }

        public ItCallRequestLog ReturnCallRequestLog(Guid logid, string user)
        {

            var log = _metro.ItCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now + " Пользователь " + user + " вернул заявку на доработку;";
            return log;
        }

        public ItCallRequestLog CloseCallRequestLog(Guid logid, string user)
        {
            var log = _metro.ItCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now + " Пользователь " + user + " закрыл заявку;";
            return log;
        }

        private static List<string> ParseCallRequestLogRecords(string logtext)
        {
            List<string> ncollectiontext = Regex.Split(logtext.Substring(0, logtext.Length - 1), ";").ToList();
            return ncollectiontext;
        }
      

    }
}