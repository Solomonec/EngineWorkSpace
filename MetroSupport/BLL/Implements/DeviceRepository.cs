using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeviceBase.Models;
using MetroSupport.BLL.Interfaces;

namespace MetroSupport.BLL.Implements
{
    public class DeviceRepository:IDeviceRepository
    {
        private readonly DeviceContext _context;

        public DeviceRepository(DeviceContext context)
        {
            _context = context;
        }

        public IQueryable<ItDevice> GetItDevicesByInventoryNumber(string invnumber)
        {
           return _context.ItDevices.Where(x => x.DevInvNum.Contains(invnumber) || x.DevBuhInvNumber.Contains(invnumber));
        }

        public IQueryable<AsppDevice> GetAsppDevicesByInventoryNumber(string invnumber)
        {
            return _context.AsppDevices.Where(x => x.DevInvNum.Contains(invnumber) || x.DevBuhInvNumber.Contains(invnumber));
        }

        public IQueryable<PaDevice> GetPaDevicesByInventoryNumber(string invnumber)
        {
            return _context.PaDevices.Where(x => x.DevInvNum.Contains(invnumber) || x.DevBuhInvNumber.Contains(invnumber));
        }
    }
}