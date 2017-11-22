using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPaging;
using MetroSupport.Models;

namespace MetroSupport.Areas.Administration.ViewModels
{
    public class PaCategoryViewModel
    {
        public IPagedList<PaCategory> PaCategories { get; set; }
        public PaCategory PaCategory { get; set; }
         public PaCategoryViewModel()
        {
            PaCategory = new PaCategory();
        }
    }
}