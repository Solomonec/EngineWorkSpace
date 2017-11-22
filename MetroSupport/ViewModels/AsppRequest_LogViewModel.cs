using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MetroSupport.Models;

namespace MetroSupport.ViewModels
{
    public class AsppRequest_LogViewModel
    {
        public AsppCallRequest AsppRequest { get; set; }
    
        public LogViewModel AsppRequestLog { get; set; }

        public SelectList Location { get; set; }

        public UserProfile User { get; set; }

        public AsppRequest_LogViewModel() {

            AsppRequest = new AsppCallRequest();
            AsppRequestLog = new LogViewModel();
        }
    
    }
}