using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MetroSupport.BLL.Interfaces;
using MetroSupport.Models;
using MetroSupport.ViewModels;

namespace MetroSupport.BLL.Implements
{
    public class ItCategoryRepository: ICategoryRepository<ItCategory>
    {
        private readonly MetroSupportContext _metro;
        public ItCategoryRepository(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public IQueryable<ItCategory> GetAllCategories()
        {
            return _metro.ItCategories.OrderBy(x => x.CategoryIndexator);
        }

        public IQueryable<ItCategory> GetAllBaseCategories()
        {
            return _metro.ItCategories.Where(x => x.CategoryType == "Base").OrderBy(x=>x.CategoryIndexator);
        }

        public IQueryable<ItCategory> GetAllSubCategories()
        {
            return _metro.ItCategories.Where(x => x.CategoryType == "Sub").OrderBy(x => x.CategoryIndexator);
        }

        public ItCategory GetCategoryById(string id)
        {
            if (id != String.Empty)
            {
                Guid guid = Guid.Parse(id);
                
                return _metro.ItCategories.FirstOrDefault(x => x.CategoryId == guid);
            }
            return null;
        }

        public IQueryable<ItCategory> GetSubCategoriesByCategory(string categoryindex)
        {
            if (categoryindex != String.Empty)
            {
                return _metro.ItCategories.Where(x => x.CategoryIndexator == categoryindex).OrderBy(x=>x.CategoryName);
            }
            return null;
        }

        public bool CreateNewCategory(ItCategory category)
        {
            if (category != null)
            {
                _metro.ItCategories.Add(category);
                _metro.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SaveCategoryChanges(ItCategory category)
        {

            ItCategory currentcategory = _metro.ItCategories.FirstOrDefault(x => x.CategoryId == category.CategoryId);
            if (currentcategory != null)
            {
                currentcategory.CategoryIndexator = currentcategory.CategoryIndexator;
                currentcategory.CategoryName = currentcategory.CategoryName;
                currentcategory.ModelIndexator = currentcategory.ModelIndexator;
                currentcategory.CategoryType = currentcategory.CategoryType;
                currentcategory.NextSubCategory = currentcategory.NextSubCategory;
                _metro.Entry(currentcategory).State = EntityState.Modified;
                _metro.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool DeleteCategory(string categoryid)
        {
            
            if (categoryid != String.Empty)
            {
                Guid guid = Guid.Parse(categoryid);
                ItCategory category = _metro.ItCategories.FirstOrDefault(x => x.CategoryId == guid);
                if(category != null)
                { 
                _metro.Entry(category).State = EntityState.Deleted;
                _metro.SaveChanges();
                return true;
                }
                return false;
            }
            return false;
        }
    }
}