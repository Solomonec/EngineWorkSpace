using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPaging;
using MetroSupport.Models;

namespace MetroSupport.Areas.Administration.ViewModels
{
    public class AsppCategoryViewModel
    {
        public IPagedList<AsppCategory> AsppCategories { get; set; }
        public AsppCategory AsppCategory { get; set; }

        public AsppCategoryViewModel()
        {
            AsppCategory = new AsppCategory();
        }
    }
}