using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MetroSupport.BLL.Interfaces;
using MetroSupport.Models;

namespace MetroSupport.BLL.Implements
{
    public class PaCategoryRepository:ICategoryRepository<PaCategory>
    {
        private readonly MetroSupportContext _metro;
        public PaCategoryRepository(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public IQueryable<PaCategory> GetAllCategories()
        {
            return _metro.PaCategories.OrderBy(x=>x.CategoryIndexator);
        }

        public IQueryable<PaCategory> GetAllBaseCategories()
        {
            return _metro.PaCategories.Where(x => x.CategoryType == "Base").OrderBy(x => x.CategoryIndexator);
        }

        public IQueryable<PaCategory> GetAllSubCategories()
        {
            return _metro.PaCategories.Where(x => x.CategoryType == "Sub").OrderBy(x => x.CategoryIndexator);
        }

        public PaCategory GetCategoryById(string id)
        {
            if (id != String.Empty)
            {
                Guid guid = Guid.Parse(id);
                return _metro.PaCategories.FirstOrDefault(x => x.CategoryId == guid);
            }
            return null;
        }

        public IQueryable<PaCategory> GetSubCategoriesByCategory(string categoryindex)
        {
            if (categoryindex != String.Empty)
            {
                return _metro.PaCategories.Where(x => x.CategoryIndexator == categoryindex);
            }
            else return null;
        }

        public bool CreateNewCategory(PaCategory category)
        {
            if (category != null)
            {
                _metro.PaCategories.Add(category);
                _metro.SaveChanges();

                return true;
            }
            else return false;
        }

        public bool SaveCategoryChanges(PaCategory category)
        {
            PaCategory currentcategory = _metro.PaCategories.FirstOrDefault(x => x.CategoryId == category.CategoryId);
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
                PaCategory Category = _metro.PaCategories.FirstOrDefault(x => x.CategoryId == guid);
                if (Category != null)
                {
                    _metro.Entry(Category).State = EntityState.Deleted;
                    _metro.SaveChanges();
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}