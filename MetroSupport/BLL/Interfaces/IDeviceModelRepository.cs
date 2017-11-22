using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroSupport.Models;

namespace MetroSupport.BLL.Interfaces
{
    public interface IDeviceModelRepository
    {
        IQueryable<DeviceModel> GetDevicesModels();
        IQueryable<DeviceModel> GetDevicesModelsByDepartment(string department);
        IQueryable<DeviceModel> GetDevicesModelsByIndexator(string indexator);
        DeviceModel GetDeviceModelById(string id);
        bool CreateNewDeviceModel(DeviceModel devicemodel);
        bool EditDeviceModel(DeviceModel devicemodel);
        bool DeleteDeviceModel(string id);
    }
}