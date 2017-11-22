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
    public class AsppCallRequestLogRepository:ILogCallRequestRepository<AsppCallRequestLog>
    {
        private readonly MetroSupportContext _metro;

        public AsppCallRequestLogRepository(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public LogViewModel RequestLogToViewModel(AsppCallRequestLog log)
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

        public AsppCallRequestLog GetCallRequestLogById(string reqid)
        {
            if (reqid != String.Empty)
            {
                Guid guid = Guid.Parse(reqid);
                AsppCallRequestLog log = _metro.AsppCallRequestLogs.FirstOrDefault(x => x.LogId == guid);
                return log;
            }
            return null;
        }

        public AsppCallRequestLog CreateCallRequestLog(Guid id, string user)
        {
            AsppCallRequestLog nlog = new AsppCallRequestLog
            {
                LogId = id,
                CreationDate = DateTime.Now,
                Creator = user,
                LastUpdateDate = DateTime.Now,
                WhoLastUpdate = user,
                LogText = DateTime.Now + " Пользователь " + user + " создал заявку;"
            };
            return nlog;

        }

        public AsppCallRequestLog ChangeCallRequestLog(Guid logid, string user)
        {
            var log = _metro.AsppCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now + " Пользователь " + user + " внес изменения в информацию о заявке;";
            return log;
        }

        public AsppCallRequestLog SetOnHoldCallRequestLog(Guid logid, string user)
        {
            var log = _metro.AsppCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now + " Пользователь " + user + " продлил выполнение заявки;";
            return log;
        }

        public AsppCallRequestLog DenyCallRequestLog(Guid logid, string user)
        {
            var log = _metro.AsppCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now + " Пользователь " + user + " отклонил заявку;";
            return log;
        }

        public AsppCallRequestLog AcceptToWorkCallRequestLog(Guid logid, string user)
        {
            var log = _metro.AsppCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now + " Пользователь " + user + " принял заявку в работу;";
            return log;
        }

        public AsppCallRequestLog ReturnCallRequestLog(Guid logid, string user)
        {
            var log = _metro.AsppCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now + " Пользователь " + user + " вернул заявку на доработку;";
            return log;
        }

        public AsppCallRequestLog CloseCallRequestLog(Guid logid, string user)
        {
            var log = _metro.AsppCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now + " Пользователь " + user + " закрыл заявку;";
            return log;
        }

        public AsppCallRequestLog SendToApproveCallRequestLog(Guid logid, string user)
        {
            var log = _metro.AsppCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now + " Пользователь " + user + " отправил заявку на одобрение;";
            return log;
        }

        private static List<string> ParseCallRequestLogRecords(string logtext)
        {
            List<string> logcollectiontext = Regex.Split(logtext.Substring(0, logtext.Length - 1), ";").ToList();
            return logcollectiontext;
        }
    }
}