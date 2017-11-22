using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPaging;
using MetroSupport.Models;

namespace MetroSupport.Areas.Administration.ViewModels
{
    public class LocationViewModel
    {
        public IPagedList<Location> Locations { get; set; }
        public Location Location { get; set; }

        public LocationViewModel()
        {
            Location = new Location();
        }
    }
}