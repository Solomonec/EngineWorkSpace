using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPaging;
using MetroSupport.Models;

namespace MetroSupport.Areas.Administration.ViewModels
{
    public class ItCategoryViewModel
    {
        public IPagedList<ItCategory> ItCategories { get; set; }
        public ItCategory ItCategory { get; set; }
        public ItCategoryViewModel()
        {
            ItCategory = new ItCategory();
        }
    }
}