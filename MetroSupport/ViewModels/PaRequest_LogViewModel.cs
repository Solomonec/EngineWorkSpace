using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MetroSupport.Models;

namespace MetroSupport.ViewModels
{
    public class PaRequest_LogViewModel
    {
        public PaCallRequest PaRequest { get; set; }
    
        public LogViewModel PaRequestLog { get; set; }

        public SelectList Location { get; set; }

        public UserProfile User { get; set; }

        public PaRequest_LogViewModel() {

            PaRequest = new PaCallRequest();
            PaRequestLog = new LogViewModel();
        }
    
    }
}