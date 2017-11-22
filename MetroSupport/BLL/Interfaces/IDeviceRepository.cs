using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceBase.Models;

namespace MetroSupport.BLL.Interfaces
{
    public interface IDeviceRepository
    {
        IQueryable<ItDevice> GetItDevicesByInventoryNumber(string invnumber);
        IQueryable<AsppDevice> GetAsppDevicesByInventoryNumber(string invnumber);
        IQueryable<PaDevice> GetPaDevicesByInventoryNumber(string invnumber);
    }
}
