using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MetroSupport.BLL.Interfaces;
using MetroSupport.Models;

namespace MetroSupport.BLL.Implements
{
    public class RequestOwnerRepository: IRequestOwnerRepository
    {
        private readonly MetroSupportContext _metro;

        public RequestOwnerRepository(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public IQueryable<RequestOwner> GetRequestOwners()
        {
            return _metro.RequestOwners.OrderBy(x=>x.RequestorName);
        }

        public IQueryable<RequestOwner> GetRequestOwnersByLiteral(string literal)
        {
            return _metro.RequestOwners.Where(x => x.RequestorName.StartsWith(literal)).OrderBy(x => x.RequestorName);
        }

        public RequestOwner GetRequestOwnerById(string ownerid)
        {
            if (ownerid != String.Empty)
            {
                Guid guid = Guid.Parse(ownerid);
                return _metro.RequestOwners.FirstOrDefault(x => x.RequestorId == guid);

            }
            return null;
        }

        public IQueryable<RequestOwner> GetRequestOwnersByName(string ownername)
        {
            if (ownername != String.Empty)
            {
                return _metro.RequestOwners.Where(x => x.RequestorName.Contains(ownername) || x.RequestorAltName.Contains(ownername));

            }
            return null;
        }

        public async Task<IQueryable<RequestOwner>> GetRequestOwnersByNameAsync(string ownername)
        {
            if (ownername != String.Empty)
            {
                return await Task.Factory.StartNew(() => GetRequestOwnersByName(ownername));

            }
            return null;
        }

        public bool CreateNewOwner(RequestOwner owner)
        {
            if (owner != null)
            {
                _metro.RequestOwners.Add(owner);
                _metro.SaveChanges();

                return true;
            }
            return false;
        }

        public bool SaveOwnerChanges(RequestOwner owner)
        {
            RequestOwner currentowner = _metro.RequestOwners.FirstOrDefault(x => x.RequestorId == owner.RequestorId);
            if (currentowner != null)
            {
                currentowner.RequestorName = owner.RequestorName;
                currentowner.RequestorAltName = owner.RequestorAltName;
                currentowner.Job = owner.Job;
                currentowner.Organization = owner.Organization;
                currentowner.Department = owner.Department;
                currentowner.Address = owner.Address;
                currentowner.Floor = owner.Floor;
                currentowner.Room = owner.Room;
                currentowner.Tel = owner.Tel;
                _metro.Entry(currentowner).State = EntityState.Modified;
                _metro.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteOwner(string ownerid)
        {
            if (ownerid != String.Empty)
            {
                Guid guid = Guid.Parse(ownerid);
                RequestOwner owner = _metro.RequestOwners.FirstOrDefault(x => x.RequestorId == guid);
                if (owner != null)
                {
                    _metro.Entry(ownerid).State = EntityState.Deleted;
                    _metro.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}