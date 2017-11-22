using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using MetroSupport.Models;
using MetroSupport.ViewModels;

namespace MetroSupport.BLL.Interfaces.Filter
{
    public abstract class CallRequestFilterOption<T>: CallRequestFilter<T> 
    {
        protected CallRequestFilter<T> _filter;
        protected Expression<Func<T, bool>> _predicate; 
        protected CallRequestFilterOption(Expression<Func<T,bool>> predicate, IQueryable<T> requests, CallRequestFilter<T> filter) : base(requests)
        {
            _filter = filter;
            _predicate = predicate;
        }

        
    }
}