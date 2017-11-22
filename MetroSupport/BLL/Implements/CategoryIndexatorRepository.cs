using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MetroSupport.BLL.Interfaces;
using MetroSupport.Models;

namespace MetroSupport.BLL.Implements
{
    public class CategoryIndexatorRepository: ICategoryIndexatorRepository
    {
        private MetroSupportContext _metro;

        public CategoryIndexatorRepository(MetroSupportContext metro)
        {
            _metro = metro;
        }


        public IQueryable<CategoryIndexator> GetAllIndexators()
        {
            return _metro.CategoryIndexators;
        }

        public CategoryIndexator GetIndexatorsById(string id)
        {
            if (id != String.Empty)
            {
                Guid guid = Guid.Parse(id);
                return _metro.CategoryIndexators.FirstOrDefault(x => x.IndexatorId == guid);
            }
            else
                return null;
        }

        public IQueryable<CategoryIndexator> GetAllBaseCategories(string department)
        {
            return _metro.CategoryIndexators.Where(x => x.CategoryType == "Base" && x.Department == department);
        }

        public IQueryable<CategoryIndexator> GetAllSubCategories(string department)
        {
            return _metro.CategoryIndexators.Where(x => x.CategoryType == "Sub" && x.Department == department);
        }

        public IQueryable<CategoryIndexator> GetIndexatorsByDepartment(string department)
        {
            if (department != String.Empty)
                return _metro.CategoryIndexators.Where(x => x.Department == department).OrderBy(x=>x.CategoryIndexatorName);
            else 
                return null;
        }

        public bool CreateNewIndexator(CategoryIndexator indexator)
        {

            if (indexator != null)
            {
                _metro.CategoryIndexators.Add(indexator);
                _metro.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SaveIndexator(CategoryIndexator indexator)
        {

            CategoryIndexator currentindexator = _metro.CategoryIndexators.FirstOrDefault(x => x.IndexatorId == indexator.IndexatorId);
            if (currentindexator != null)
            {
                currentindexator.CategoryIndexatorName = indexator.CategoryIndexatorName;
                currentindexator.Department = indexator.Department;
                _metro.Entry(currentindexator).State = EntityState.Modified;
                _metro.SaveChanges();
                return true;
            }
            else return false;
         
        }

        public bool DeleteIndexator(string indexatorid)
        {
            if (indexatorid != String.Empty)
            {
                Guid guid = Guid.Parse(indexatorid);
                CategoryIndexator indexator = _metro.CategoryIndexators.FirstOrDefault(x => x.IndexatorId == guid);
                if (indexator != null)
                {
                    _metro.Entry(indexator).State = EntityState.Deleted;
                    _metro.SaveChanges();
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}