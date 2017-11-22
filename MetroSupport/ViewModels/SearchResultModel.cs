using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetroSupport.ViewModels
{
    public class SearchResultModel
    {
        public Guid CallRequestId { get; set; }
        public DateTime? Creation { get; set; }
        public DateTime? Time { get; set; }
        public string RequestNumber { get; set; }
        public string Status { get; set; }
        public string RequestorName { get; set; }
        public string AssignTo { get; set; }
        public string AssignBoss { get; set; }
        public string Category { get; set; }
        public string TroubleSubject { get; set; }
        public string Comment { get; set; }
        
    }
}
