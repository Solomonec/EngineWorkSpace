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
    public class PaCallRequestLogRepository: ILogCallRequestRepository<PaCallRequestLog>
    {
        private readonly MetroSupportContext _metro;

        public PaCallRequestLogRepository(MetroSupportContext metro)
        {
            _metro = metro;
        }


        public PaCallRequestLog SendToApproveCallRequestLog(Guid logid, string user)
        {
            var log = _metro.PaCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now + " Пользователь " + user + " отправил заявку на одобрение;";
            return log;
        }

        public LogViewModel RequestLogToViewModel(PaCallRequestLog log)
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

        public PaCallRequestLog GetCallRequestLogById(string reqid)
        {
            if (reqid != String.Empty)
            {
                Guid guid = Guid.Parse(reqid);
                PaCallRequestLog log = _metro.PaCallRequestLogs.FirstOrDefault(x => x.LogId == guid);
                return log;
            }
            return null;
        }

        public PaCallRequestLog CreateCallRequestLog(Guid id, string user)
        {
            PaCallRequestLog nlog = new PaCallRequestLog
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

        public PaCallRequestLog ChangeCallRequestLog(Guid logid, string user)
        {
            var log = _metro.PaCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now.ToString() + " Пользователь " + user + " внес изменения в информацию о заявке;";
            return log;
        }

        public PaCallRequestLog SetOnHoldCallRequestLog(Guid logid, string user)
        {
            var log = _metro.PaCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now + " Пользователь " + user + " продлил выполнение заявки;";
            return log;
        }

        public PaCallRequestLog DenyCallRequestLog(Guid logid, string user)
        {
            var log = _metro.PaCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now + " Пользователь " + user + " отклонил заявку;";
            return log;
        }

        public PaCallRequestLog AcceptToWorkCallRequestLog(Guid logid, string user)
        {
            var log = _metro.PaCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now + " Пользователь " + user + " принял заявку в работу;";
            return log;
        }

        public PaCallRequestLog ReturnCallRequestLog(Guid logid, string user)
        {
            var log = _metro.PaCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
            log.LastUpdateDate = DateTime.Now;
            log.WhoLastUpdate = user;
            log.LogText += DateTime.Now + " Пользователь " + user + " вернул заявку на доработку;";
            return log;
        }

        public PaCallRequestLog CloseCallRequestLog(Guid logid, string user)
        {
            var log = _metro.PaCallRequestLogs.FirstOrDefault(x => x.LogId == logid);
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