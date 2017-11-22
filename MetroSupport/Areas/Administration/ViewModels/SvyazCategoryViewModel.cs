using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPaging;
using MetroSupport.Models;

namespace MetroSupport.Areas.Administration.ViewModels
{
    public class SvyazCategoryViewModel
    {
        public IPagedList<SvyazCategory> SvyazCategories { get; set; }
        public SvyazCategory SvyazCategory { get; set; }
        public SvyazCategoryViewModel()
        {
            SvyazCategory = new SvyazCategory();
        }
    }
}