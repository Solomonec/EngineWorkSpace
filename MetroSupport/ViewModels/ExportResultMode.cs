using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MetroSupport.ViewModels
{
    public class ExportResultModel
    {
        public string CreationDate { get; set; }
        public string CreationTime { get; set; }
        public string RequestNumber { get; set; }
        public string Status { get; set; }
        public string RequestorName { get; set; }
        public string AssignBoss { get; set; }
        public string AssignTo { get; set; }
        public string Category { get; set; }
        public string TroubleSubject { get; set; }
        public string TroubleReason { get; set; }
        public string SubCategory1 { get; set; }
        public string SubCategory2 { get; set; }
        public string SubCategory3 { get; set; }
        public string SubCategory4 { get; set; }
        public string SubCategory5 { get; set; }
        public string Organization { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string TroubleDescription { get; set; }
        public string TroubleSolution { get; set; }
        
    }
}
