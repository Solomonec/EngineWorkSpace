using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroSupport.Models;
namespace MetroSupport.BLL.Interfaces
{
    public interface ILocationRepository
    {
        IQueryable<Location> GetAllLocations();

        Location GetLocation(string locid);
        bool CreateNewLocation(Location location);
        bool DeleteLocation(string locid);

    }
}