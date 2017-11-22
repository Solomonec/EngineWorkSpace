using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MetroSupport.ViewModels;

namespace MetroSupport.BLL.Interfaces
{
    public interface IMetroSearch
    {
        IQueryable<SearchResultModel> AdvaceSearch(SearchEntry entry, FilterViewModel filtermodel);
        IQueryable<SearchResultModel> SimpleSearch(SearchEntry entry, string searchvalue);

    }

  
   public enum SearchEntry
   {
        It, Aspp, Pa, Svyaz
   }

}
