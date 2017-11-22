using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroSupport.Commons
{
    public static class Dates
    {
        public static string DateNullConversion(DateTime? date)
        {
            string result = !date.HasValue ? String.Empty : date.Value.ToShortDateString();
            return result;
        }

        public static string TimeNullConversion(DateTime? date)
        {
            string result = !date.HasValue ? String.Empty : date.Value.ToShortTimeString();
            return result;
        }
       
    }
}