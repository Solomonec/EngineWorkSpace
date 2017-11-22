using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MetroSupport.BLL.Interfaces;
using MetroSupport.Models;

namespace MetroSupport.BLL.Implements
{
    public class AssignerRepository:IAssignerRepository
    {
        public MetroSupportContext _metro;

        public AssignerRepository(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public IQueryable<Assigner> GetAllAssigners()
        {
            return _metro.Assigners;
        }

        public Assigner GetAssignerById(string assignerid)
        {
            if (assignerid != String.Empty)
            {
                Guid guidid = Guid.Parse(assignerid);
                return _metro.Assigners.FirstOrDefault(x => x.AssignerId == guidid);
            }
            return null;
        }

        public IQueryable<Assigner> GetAssignersByOrganization(string department)
        {
            if (department != String.Empty)
            {
                return _metro.Assigners.Where(x => x.Organization == department).OrderBy(x=>x.AssignerName);
            }
            return null;
        }

        public IQueryable<Assigner> GetAssignersByDepartment(string department)
        {
            if (department != String.Empty)
            {
                return _metro.Assigners.Where(x => x.Department == department).OrderBy(x => x.AssignerName);
            }
            return null;
        }

        public bool CreateNewAssigner(Assigner assigner)
        {
            if (assigner != null)
            {
                _metro.Assigners.Add(assigner);
                _metro.SaveChanges();

                return true;
            }
            else return false;
        }

        public bool SaveAssignerChanges(Assigner assigner)
        {
            Assigner Assigner = _metro.Assigners.FirstOrDefault(x => x.AssignerId == assigner.AssignerId);
            if (Assigner != null)
            {
                Assigner.AssignerName = Assigner.AssignerName;
                Assigner.Organization =Assigner.Organization;
                Assigner.BossName = Assigner.BossName;
                Assigner.Department = Assigner.Department;
                _metro.Entry(Assigner).State = EntityState.Modified;
                _metro.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool DeleteAssigner(string assignerid)
        {
            if (assignerid != String.Empty)
            {
                Guid guidid = Guid.Parse(assignerid);
                Assigner assigner = _metro.Assigners.FirstOrDefault(x => x.AssignerId == guidid);
                if (assigner != null)
                {
                    _metro.Entry(assigner).State = EntityState.Deleted;
                    _metro.SaveChanges();
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}