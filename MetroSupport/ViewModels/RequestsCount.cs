using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroSupport.ViewModels
{
    public class RequestsCount
    {
        public int InWork { set; get; }
        public int HoldOn { set; get; }
        public int Close { set; get; }

        
    }
}