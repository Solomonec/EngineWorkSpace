using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using MetroSupport.Models;
namespace MetroSupport.Areas.Administration.ViewModels
{
    public class ModelIndexatorViewModel
    {
        public IPagedList<ModelIndexator> ModelIndexators { get; set; }
        public ModelIndexator ModelIndexator { get; set; }

        public ModelIndexatorViewModel()
        {
            ModelIndexator = new ModelIndexator();
        }

    }
}
