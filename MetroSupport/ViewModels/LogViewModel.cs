using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MetroSupport.ViewModels
{
    public class LogViewModel
    {
        public string WhoCreate {get;set;}
        public DateTime CreateDate {get;set;}
        public string WhoChange { get; set; }
        public DateTime ChangeDate { get; set; }
        public List<string> LogTextCollection { get; set; }
    }
}