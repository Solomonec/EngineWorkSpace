using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using MetroSupport.Models;
namespace MetroSupport.Areas.Administration.ViewModels
{
    public class CategoryIndexatorViewModel
    {
        public IPagedList<CategoryIndexator> CategoryIndexators { get; set; }
        public CategoryIndexator CategoryIndexator { get; set; }

        public CategoryIndexatorViewModel()
        {
            CategoryIndexator = new CategoryIndexator();
        }

    }
}
