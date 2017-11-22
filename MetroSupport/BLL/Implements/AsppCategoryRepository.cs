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
    public class AsppCategoryRepository : ICategoryRepository<AsppCategory>
    {
        private readonly MetroSupportContext _metro;
        public AsppCategoryRepository(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public IQueryable<AsppCategory> GetAllCategories()
        {
            return _metro.AsppCategories.OrderBy(x=>x.CategoryIndexator);
        }


        public IQueryable<AsppCategory> GetAllBaseCategories()
        {
            return _metro.AsppCategories.Where(x => x.CategoryType == "Base").OrderBy(x => x.CategoryIndexator);
        }

        public IQueryable<AsppCategory> GetAllSubCategories()
        {
            return _metro.AsppCategories.Where(x => x.CategoryType == "Sub").OrderBy(x => x.CategoryIndexator);
        }

        public AsppCategory GetCategoryById(string id)
        {
            if (id != String.Empty)
            {
                Guid guid = Guid.Parse(id);
                return _metro.AsppCategories.FirstOrDefault(x => x.CategoryId == guid);
            }
            return null;
        }

        public IQueryable<AsppCategory> GetSubCategoriesByCategory(string categoryindex)
        {
            if (categoryindex != String.Empty)
            {
                return _metro.AsppCategories.Where(x => x.CategoryIndexator == categoryindex);
            }
            else return null;
        }

        public bool CreateNewCategory(AsppCategory category)
        {
            if (category != null)
            {
                _metro.AsppCategories.Add(category);
                _metro.SaveChanges();

                return true;
            }
            else return false;
        }

        public bool SaveCategoryChanges(AsppCategory category)
        {
            AsppCategory currentcategory = _metro.AsppCategories.FirstOrDefault(x => x.CategoryId == category.CategoryId);
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
                AsppCategory category = _metro.AsppCategories.FirstOrDefault(x => x.CategoryId == guid);
                if (category != null)
                {
                    _metro.Entry(category).State = EntityState.Deleted;
                    _metro.SaveChanges();
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}