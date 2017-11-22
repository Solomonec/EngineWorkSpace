using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Net.Mail;
using MetroSupport.BLL.Interfaces;
using MetroSupport.Models;
using MetroSupport.ViewModels;
using MetroSupport.Commons;

namespace MetroSupport.BLL.Implements
{
    public class MailNotificator: IMailNotification
    {
        public MailData GetMailData(Guid callrequestid, string requestnumber, string troublesubject, UserProfile assigner,
            UserProfile boss, string troubledepartment)
        {
            MailData mail = new MailData();
            mail.CallRequestId = callrequestid;
            mail.CallRequestNumber = requestnumber;
            mail.CallRequestTheme = troublesubject;
            mail.Assigner = assigner;
            mail.Boss = boss;
            mail.Department = troubledepartment;

            return mail;
        }

        public void SendNotificationMessage(MailData maildata)
        {

            var subject = "**MetroSupport** [{0}] Вам назначена новая заявка ( {1} )";

            var body = "<html><head></head><body><div class=\"main-requests-info-box\"><div class=\"logo-box\"> MetroSupport </div> <div class=\"content-box\"><div class=\"content-header\">" +
                         "<div class=\"cotext-header-text-block\"> <p><div class=\"cotext-header-lable\">Заявка №: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; <span class=\"header-data-text\">{0}</span></div></p>" +
                         "<span class=\"cotext-header-lable\">Тема проблемы: &nbsp; &nbsp; &nbsp; <span class=\"header-data-text\">{1}</span></span></div></div>" +
                         "<div class=\"content-data-info\">Доброго времени суток!<p> Вам назначена новая заявка: </p><p>Ответственный: &nbsp;  {2}<br> Исполнитель: &nbsp; &nbsp; &nbsp; {3}</p>" +
                         "<p>Ознакомиться с содержимым заявки <br> и начать её выполнение можно перейдя по <a href=\"http://metrosupport.metro.local/{4}CallRequest/Index?Id={5}\" class=\"request-link\">ссылке</a></p>По окончании выполнения заявки, просьба закрыть ее." +
                         "<p>С уважением, <br> поддержка MetroSupport,<br> служба ИТС. </p> <\br> </div> </div></div> {6} </body></html>";
            var style = "<style type=\"text/css\"> body{margin: 0; padding: 0; background-color: #41849C; } .main-requests-info-box { margin: 78px; width: 1027px; height: 543px; border: 6px solid #B8B8B8; background-color: #D9D9D9;}" +
                                                    " .content-box{ float: left; } .content-header {margin:0; width: 1117px; height: 116px; background-color: #C5C5C5;} .logo-box { padding:20px 0 20px 27px; width:1121px; color:white; font-family: Arial; font-size: 28px; font-weight: 700; background-color: #0065A0; }" +
                                                    " .content-data-info { padding: 27px 0 27px 27px; width: 520px; height: 298px;	font-family: Arial;	font-size: 18px; font-weight: 700; color: #353535 } .request-link {	text-decoration: none; color:#328FDC; } .cotext-header-text-block { padding: 16px 0 0 21px;	height: 100px;} " +
                                                   ".cotext-header-lable { width: 1100px; height: 27px; font-family: Arial; font-size: 24px;	font-weight: 700; color: #1E4F7E; } .header-data-text{ color: #585858; }</style>";


            var message = new MailMessage();
            if (maildata.Assigner != null)
            {
                message.To.Add(new MailAddress(maildata.Assigner.Email));
            }
            else maildata.Assigner = new UserProfile();
            message.To.Add(new MailAddress(maildata.Boss.Email)); 
            message.Subject = string.Format(subject, maildata.CallRequestNumber, maildata.CallRequestTheme);
            message.Body = string.Format(body, maildata.CallRequestNumber, maildata.CallRequestTheme, maildata.Boss.FullName, maildata.Assigner.FullName, maildata.Department.ToInternationalDepartmentName(), maildata.CallRequestId.ToString(), style);
            message.IsBodyHtml = true;



            using (var smtp = new SmtpClient())
            {
               smtp.Send(message);
            }

        }

     
    }

    
   
}