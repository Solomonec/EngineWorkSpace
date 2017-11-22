using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroSupport.Models;

namespace MetroSupport.BLL.Interfaces
{
    public interface ICategoryIndexatorRepository
    {
        IQueryable<CategoryIndexator> GetAllIndexators();
        CategoryIndexator GetIndexatorsById(string id);
        IQueryable<CategoryIndexator> GetAllBaseCategories(string department);
        IQueryable<CategoryIndexator> GetAllSubCategories(string department);
        IQueryable<CategoryIndexator> GetIndexatorsByDepartment(string department);
        bool CreateNewIndexator(CategoryIndexator indexator);
        bool SaveIndexator(CategoryIndexator indexator);
        bool DeleteIndexator(string indexatorid);
    }
}