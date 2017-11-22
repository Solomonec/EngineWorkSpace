using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using MetroSupport.BLL.Interfaces;
using MetroSupport.Models;

namespace MetroSupport.BLL.Implements
{
    public class DeviceModelRepository:IDeviceModelRepository
    {
        private readonly MetroSupportContext _metro;

        public DeviceModelRepository(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public IQueryable<DeviceModel> GetDevicesModels()
        {
            return _metro.DeviceModels;
        }

        public IQueryable<DeviceModel> GetDevicesModelsByDepartment(string department)
        {
            if (department != String.Empty)
            {
                return _metro.DeviceModels.Where(x => x.Department == department).OrderBy(x=>x.ModelIndexator);

            }
            else return null;
        }

        public IQueryable<DeviceModel> GetDevicesModelsByIndexator(string indexator)
        {
            if (indexator != String.Empty)
            {
                return _metro.DeviceModels.Where(x => x.ModelIndexator == indexator);

            }
            else return null;
        }

        public DeviceModel GetDeviceModelById(string id)
        {
            if (id != String.Empty)
            {
                Guid guid = Guid.Parse(id);
                return _metro.DeviceModels.FirstOrDefault(x => x.ModelId == guid);

            }
            else return null;
        }

        public bool CreateNewDeviceModel(DeviceModel devicemodel)
        {
            if (devicemodel != null)
            {
                _metro.DeviceModels.Add(devicemodel);
                _metro.SaveChanges();

                return true;
            }
            else return false;
        }

        public bool EditDeviceModel(DeviceModel devicemodel)
        {
            DeviceModel currentdevicemodel = _metro.DeviceModels.FirstOrDefault(x => x.ModelId == devicemodel.ModelId);
            if (currentdevicemodel != null)
            {
                currentdevicemodel.ModelIndexator = devicemodel.ModelIndexator;
                currentdevicemodel.ModelName = devicemodel.ModelName;
                currentdevicemodel.Department = devicemodel.Department;
                _metro.Entry(devicemodel).State = EntityState.Modified;
                _metro.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool DeleteDeviceModel(string id)
        {
            if (id != String.Empty)
            {
                Guid guid = Guid.Parse(id);
                DeviceModel device = _metro.DeviceModels.FirstOrDefault(x => x.ModelId == guid);
                if (device != null)
                {
                    _metro.Entry(device).State = EntityState.Deleted;
                    _metro.SaveChanges();
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}