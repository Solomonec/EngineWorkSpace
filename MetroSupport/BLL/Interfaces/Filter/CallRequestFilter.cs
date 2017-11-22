using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.DynamicProxy.Generators.Emitters.CodeBuilders;
using MetroSupport.Models;
using MetroSupport.ViewModels;

namespace MetroSupport.BLL.Interfaces.Filter
{
    public abstract class CallRequestFilter<T>
    {
        protected IQueryable<T> _requests { get; set; }

        protected CallRequestFilter(IQueryable<T> requests)
        {
            _requests = requests;
        }
        
        public abstract IQueryable<T> FilterResult();
        
    }
}