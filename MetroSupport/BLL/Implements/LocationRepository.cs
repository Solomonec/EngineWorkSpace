using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MetroSupport.BLL.Interfaces;
using MetroSupport.Models;

namespace MetroSupport.BLL.Implements
{
    public class LocationRepository: ILocationRepository 
    {

        private readonly MetroSupportContext _metro;
        public LocationRepository(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public IQueryable<Location> GetAllLocations()
        {
            return _metro.Locations.OrderBy(x=>x.LocationName);
        }
        public Location GetLocation(string locid)
        {
            if (locid != String.Empty)
            {
                Guid guid = Guid.Parse(locid);
                return _metro.Locations.FirstOrDefault(x => x.LocationId == guid);
            }
            else return null;
        }

       
        public bool CreateNewLocation(Location location)
        {
            if (location != null)
            {
                _metro.Locations.Add(location);
                _metro.SaveChanges();
              return true;    
            }
            return false;
        }

        public bool DeleteLocation(string locid)
        {
            if (!string.IsNullOrEmpty(locid))
            {
                Guid guid = Guid.Parse(locid);
                Location location = _metro.Locations.FirstOrDefault(x => x.LocationId == guid);
                if (location != null)
                {
                    _metro.Entry(location).State = EntityState.Deleted;
                    _metro.SaveChanges();
                    return true;
                }
                
            }
            return false;
        }
    }
}