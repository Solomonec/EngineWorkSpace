using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroSupport.Models;

namespace MetroSupport.BLL.Interfaces
{
    public interface IModelIndexatorRepository
    {
        IQueryable<ModelIndexator> GetAllIndexators();
        ModelIndexator GetIndexatorById(string id);
        IQueryable<ModelIndexator> GetIndexatorsByDepartment(string department);
        bool CreateNewIndexator(ModelIndexator indexator);
        bool SaveIndexator(ModelIndexator indexator);
        bool DeleteIndexator(string indexatorid);
    }
}