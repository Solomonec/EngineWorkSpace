using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MetroSupport.Models;

namespace MetroSupport.ViewModels
{
    public class ItRequest_LogViewModel
    {
        public ItCallRequest ItRequest { get; set; }
    
        public LogViewModel ItRequestLog { get; set; }

        public SelectList Location { get; set; }

        public UserProfile User { get; set; }

        public ItRequest_LogViewModel() {

            ItRequest = new ItCallRequest();
            ItRequestLog = new LogViewModel();
        }
    
    }
}