using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroSupport.BLL.Interfaces.Filter;
using MetroSupport.Models;
using MetroSupport.ViewModels;

namespace MetroSupport.BLL.Implements.Filter
{
    public class MetroCallRequestFilter<T>:CallRequestFilter<T> where T:class
    {
        public MetroCallRequestFilter(IQueryable<T> requests): base(requests)
        {

        }

        public override IQueryable<T> FilterResult()
        {
            return _requests;
        }
    }
}