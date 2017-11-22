using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MetroSupport.BLL.Interfaces;
using MetroSupport.Models;

namespace MetroSupport.BLL.Implements
{
    public class ModelIndexatorRepository: IModelIndexatorRepository
    {
        private readonly MetroSupportContext _metro;

        public ModelIndexatorRepository(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public IQueryable<ModelIndexator> GetAllIndexators()
        {
            return _metro.ModelIndexators.OrderBy(x=>x.Department);
        }

        public ModelIndexator GetIndexatorById(string id)
        {
            if (id != String.Empty)
            {
                Guid guid = Guid.Parse(id);
                return _metro.ModelIndexators.FirstOrDefault(x => x.IndexatorId == guid);
            }
            else
                return null;
        }

        public IQueryable<ModelIndexator> GetIndexatorsByDepartment(string department)
        {
            if (department != String.Empty)
                return _metro.ModelIndexators.Where(x => x.Department == department).OrderBy(x => x.ModelIndexatorName);
            else
                return null;
        }

        public bool CreateNewIndexator(ModelIndexator indexator)
        {
            if (indexator != null)
            {
                _metro.ModelIndexators.Add(indexator);
                _metro.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SaveIndexator(ModelIndexator indexator)
        {
            ModelIndexator currentindexator = _metro.ModelIndexators.FirstOrDefault(x => x.IndexatorId == indexator.IndexatorId);
            if (currentindexator != null)
            {
                currentindexator.ModelIndexatorName = indexator.ModelIndexatorName;
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
                ModelIndexator indexator = _metro.ModelIndexators.FirstOrDefault(x => x.IndexatorId == guid);
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