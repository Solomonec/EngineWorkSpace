using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPaging;
using MetroSupport.Models;

namespace MetroSupport.Areas.Administration.ViewModels
{
    public class AssignerViewModel
    {
        public IPagedList<Assigner> Assigners { get; set; }
        public Assigner Assigner { get; set; }
        public AssignerViewModel()
        {
            Assigner = new Assigner();
            
        }
    }
}