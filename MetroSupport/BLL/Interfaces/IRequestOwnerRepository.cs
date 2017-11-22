using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MetroSupport.Models;

namespace MetroSupport.BLL.Interfaces
{
    public interface IRequestOwnerRepository
    {
        IQueryable<RequestOwner> GetRequestOwners();
        RequestOwner GetRequestOwnerById(string ownerid);
        IQueryable<RequestOwner> GetRequestOwnersByLiteral(string literal);
        IQueryable<RequestOwner> GetRequestOwnersByName(string ownername);
        Task<IQueryable<RequestOwner>> GetRequestOwnersByNameAsync(string ownername);
        bool CreateNewOwner(RequestOwner owner);
        bool SaveOwnerChanges(RequestOwner owner);
        bool DeleteOwner(string ownerid);

    }
}