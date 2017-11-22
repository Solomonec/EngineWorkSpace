using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroSupport.BLL.Interfaces;
using MetroSupport.Commons;
using MetroSupport.Models;
using MetroSupport.ViewModels;

namespace MetroSupport.BLL.Implements
{
    public class DataFilter:IDataFilter
    {
          private MetroSupportContext _metro;

          public DataFilter(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public IQueryable<ExportResultModel> Filter(SearchEntry entry, FilterViewModel filtermodel)
        {
            switch (entry)
            {
                case SearchEntry.It: return new ItCallRequestSearch(_metro).AdvanceSearch(filtermodel).ToExportResultModel();
                case SearchEntry.Aspp: return new AsppCallRequestSearch(_metro).AdvanceSearch(filtermodel).ToExportResultModel();
                case SearchEntry.Svyaz: return new SvyazCallRequestSearch(_metro).AdvanceSearch(filtermodel).ToExportResultModel();
                case SearchEntry.Pa: return new PaCallRequestSearch(_metro).AdvanceSearch(filtermodel).ToExportResultModel();
                default: return null;
            }

        }
    }
}