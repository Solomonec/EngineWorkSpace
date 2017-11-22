using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPaging;
using MetroSupport.Models;

namespace MetroSupport.Areas.Administration.ViewModels
{
    public class DeviceModelsViewModel
    {
        public IPagedList<DeviceModel> DeviceModels { get; set; }
        public DeviceModel DeviceModel { get; set; }

        public DeviceModelsViewModel()
        {
            DeviceModel = new DeviceModel();
        }
    }
}