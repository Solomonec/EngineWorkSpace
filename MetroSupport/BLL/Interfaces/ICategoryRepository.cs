using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroSupport.BLL.Interfaces
{
    public interface ICategoryRepository<T>
    {
        IQueryable<T> GetAllCategories();
        IQueryable<T> GetAllBaseCategories();
        IQueryable<T> GetAllSubCategories();
        T GetCategoryById(string id);
        IQueryable<T> GetSubCategoriesByCategory(string categoryindex);
        bool CreateNewCategory(T category);
        bool SaveCategoryChanges(T category);
        bool DeleteCategory(string categoryid);

    }
}