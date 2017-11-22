using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MetroSupport.ViewModels;

namespace MetroSupport.BLL.Interfaces
{
    public interface IDataFilter
    {
        IQueryable<ExportResultModel> Filter(SearchEntry entry, FilterViewModel filtermodel);

    }
}
