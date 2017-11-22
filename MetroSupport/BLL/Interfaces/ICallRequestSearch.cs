using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using MetroSupport.ViewModels;

namespace MetroSupport.BLL.Interfaces
{
    public interface ICallRequestSearch<T>
    {

        IQueryable<T> SimpleSearch(string searchvalue);

        IQueryable<T> AdvanceSearch(FilterViewModel filtermodel);


    }
}