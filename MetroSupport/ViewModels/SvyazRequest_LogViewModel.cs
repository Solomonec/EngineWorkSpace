using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MetroSupport.Models;

namespace MetroSupport.ViewModels
{
    public class SvyazRequest_LogViewModel
    {
        public SvyazCallRequest SvyazRequest { get; set; }

        public LogViewModel SvyazRequestLog { get; set; }

        public SelectList Location { get; set; }

        public UserProfile User { get; set; }

        public SvyazRequest_LogViewModel()
        {

            SvyazRequest = new SvyazCallRequest();
            SvyazRequestLog = new LogViewModel();
        }
    
    }
}