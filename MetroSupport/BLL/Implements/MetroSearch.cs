using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroSupport.BLL.Implements;
using MetroSupport.BLL.Interfaces;
using MetroSupport.Commons;
using MetroSupport.Models;
using MetroSupport.ViewModels;

namespace MetroSupport.BLL
{
    public class MetroSearch:IMetroSearch
    {
        private MetroSupportContext _metro;

        public MetroSearch(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public IQueryable<SearchResultModel> AdvaceSearch(SearchEntry entry, FilterViewModel filtermodel)
        {
            switch (entry)
            {
                case SearchEntry.It: return new ItCallRequestSearch(_metro).AdvanceSearch(filtermodel).ToSearchResultModel();
                case SearchEntry.Aspp: return new AsppCallRequestSearch(_metro).AdvanceSearch(filtermodel).ToSearchResultModel();
                case SearchEntry.Svyaz: return new SvyazCallRequestSearch(_metro).AdvanceSearch(filtermodel).ToSearchResultModel();
                case SearchEntry.Pa: return new PaCallRequestSearch(_metro).AdvanceSearch(filtermodel).ToSearchResultModel();
                default: return null;
            }

        }

        public IQueryable<SearchResultModel> SimpleSearch(SearchEntry entry, string searchvalue)
        {
            
            switch (entry)
            {
                case SearchEntry.It:return new ItCallRequestSearch(_metro).SimpleSearch(searchvalue).ToSearchResultModel();
                case SearchEntry.Aspp: return new AsppCallRequestSearch(_metro).SimpleSearch(searchvalue).ToSearchResultModel();
                case SearchEntry.Svyaz: return new SvyazCallRequestSearch(_metro).SimpleSearch(searchvalue).ToSearchResultModel();
                case SearchEntry.Pa: return new PaCallRequestSearch(_metro).SimpleSearch(searchvalue).ToSearchResultModel();
                default: return null;
            }


        }
    }
}