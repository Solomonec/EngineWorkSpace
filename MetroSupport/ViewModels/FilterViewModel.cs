using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroSupport.Models;

namespace MetroSupport.ViewModels
{
    public class FilterViewModel
    {
        public string Assigner { get; set; }
        public string Boss { get; set; }
        public string Requestor { get; set; }
        public string TroubleTheme { get; set; }
        public string TroubleReason { get; set; }
        public string Prevention { get; set; }
        public int Status { get; set; }
        public string DeviceName { get; set; }
        public string DeviceModel { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        
    }
}