using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroSupport.Models;

namespace MetroSupport.BLL.Interfaces
{
    public interface IAssignerRepository
    {
        IQueryable<Assigner> GetAllAssigners();
        Assigner GetAssignerById(string assignerid);
        IQueryable<Assigner> GetAssignersByDepartment(string department);
        IQueryable<Assigner> GetAssignersByOrganization(string department);
        bool CreateNewAssigner(Assigner assigner);
        bool SaveAssignerChanges(Assigner assigner);
        bool DeleteAssigner(string assignerid);

    }
}