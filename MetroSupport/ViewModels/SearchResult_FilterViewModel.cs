using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroSupport.ViewModels
{
    public class SearchResult_FilterViewModel
    {
        public FilterViewModel Filter { get; set; }
        public IQueryable<SearchResultModel> SearchResult { get; set; }

        public SearchResult_FilterViewModel()
        {
            Filter = new FilterViewModel();
        }

    }
}