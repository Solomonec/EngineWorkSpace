using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroSupport.ViewModels
{
    public class TopCallRequest
    {
        public string RequestId { get; set; }
        public string RequestNumber { get; set; }
        public string Theme { get; set; }
        public string Department { get; set; }
    }
}