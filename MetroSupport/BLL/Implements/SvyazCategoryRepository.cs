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
    public class SvyazCategoryRepository: ICategoryRepository<SvyazCategory>
    {
        private readonly MetroSupportContext _metro;
        public SvyazCategoryRepository(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public IQueryable<SvyazCategory> GetAllCategories()
        {
            return _metro.SvyazCategories.OrderBy(x=>x.CategoryIndexator);
        }

        public IQueryable<SvyazCategory> GetAllBaseCategories()
        {
            return _metro.SvyazCategories.Where(x => x.CategoryType == "Base").OrderBy(x => x.CategoryIndexator); ;
        }

        public IQueryable<SvyazCategory> GetAllSubCategories()
        {
            return _metro.SvyazCategories.Where(x => x.CategoryType == "Sub").OrderBy(x=>x.CategoryIndexator);;
        }

        public SvyazCategory GetCategoryById(string id)
        {
            if (id != String.Empty)
            {
                Guid guid = Guid.Parse(id);
                return _metro.SvyazCategories.FirstOrDefault(x => x.CategoryId == guid);
            }
            return null;
        }

        public IQueryable<SvyazCategory> GetSubCategoriesByCategory(string categoryindex)
        {
            if (categoryindex != String.Empty)
            {
                return _metro.SvyazCategories.Where(x => x.CategoryIndexator == categoryindex);
            }
            else return null;
        }

        public bool CreateNewCategory(SvyazCategory category)
        {
            if (category != null)
            {
                    _metro.SvyazCategories.Add(category);
                    _metro.SaveChanges();
               
                return true;
            }
            else return false;
        }

        public bool SaveCategoryChanges(SvyazCategory category)
        {

            SvyazCategory currentcategory = _metro.SvyazCategories.FirstOrDefault(x => x.CategoryId == category.CategoryId);
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
                SvyazCategory category = _metro.SvyazCategories.FirstOrDefault(x => x.CategoryId == guid);
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