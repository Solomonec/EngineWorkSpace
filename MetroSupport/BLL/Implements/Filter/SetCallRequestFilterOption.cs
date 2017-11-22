using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using MetroSupport.BLL.Interfaces.Filter;
using MetroSupport.Models;
using MetroSupport.ViewModels;

namespace MetroSupport.BLL.Implements.Filter
{
    public class SetCallRequestFilterOption<T>: CallRequestFilterOption<T> where T:class 
    {
        public SetCallRequestFilterOption(Expression<Func<T, bool>> prepicate, IQueryable<T> requests, CallRequestFilter<T> filter)
            : base(prepicate, requests, filter)
        {

        }

        public override IQueryable<T> FilterResult()
        {
            return _filter.FilterResult().Where(_predicate);
        }
    }
}