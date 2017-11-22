using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroSupport.BLL.Interfaces;
using MetroSupport.Models;
namespace MetroSupport.BLL.Services
{
    public class MetroSupportService
    {

        IRepositoryFactory _repositoryfactory;
        public MetroSupportService(IRepositoryFactory repositoryfactory)
        {
            _repositoryfactory = repositoryfactory;         
        }

        public ICallRequestRepository<ItCallRequest> ItCallRepository
        {
            get { return _repositoryfactory.ItCallRequestRepositoryCreate(); }
        }

        
        public ICallRequestRepository<SvyazCallRequest> SvyazCallRepository
        {
            get{return _repositoryfactory.SvyazCallRequestRepositoryCreate(); }
        }

        
        public ICallRequestRepository<AsppCallRequest> AsppCallRepository
        {
            get{return _repositoryfactory.AsppCallRequestRepositoryCreate(); }
        }

        
        public ICallRequestRepository<PaCallRequest> PaCallRepository
        {
            get{return _repositoryfactory.PaCallRequestRepositoryCreate(); }
        }

        public ILocationRepository LocationRepository
        {
            get { return _repositoryfactory.LocationRepositoryCreate(); }
        }

        public IDepartmentRepository DepartmentRepository
        {
            get { return _repositoryfactory.DepartmentRepositoryCreate(); }
        }
        public IDeviceModelRepository DeviceModelRepository
        {
            get { return _repositoryfactory.DeviceModelRepositoryCreate(); }
        }

        public ICategoryRepository<ItCategory> ItCategoryRepository
        {
            get { return _repositoryfactory.ItCategoryRepositoryCreate(); }
        }
        public ICategoryRepository<SvyazCategory> SvyazCategoryRepository
        {
            get { return _repositoryfactory.SvyazCategoryRepositoryCreate(); }
        }
        public ICategoryRepository<AsppCategory> AsppCategoryRepository
        {
            get { return _repositoryfactory.AsppCategoryRepositoryCreate(); }
        }
        public ICategoryRepository<PaCategory> PaCategoryRepository
        {
            get { return _repositoryfactory.PaCategoryRepositoryCreate(); }
        }
        public ICategoryIndexatorRepository CategoryIndexatorRepository
        {
            get { return _repositoryfactory.CategoryIndexatorRepositoryCreate(); }
        }
        public IModelIndexatorRepository ModelIndexatorRepository
        {
            get { return _repositoryfactory.ModelIndexatorRepositoryCreate(); }
        }
        public IBossRepository BossRepository
        {
            get { return _repositoryfactory.BossRepositoryCreate(); }
        }

        public IAssignerRepository AssignerRepository
        {
            get { return _repositoryfactory.AssignerRepositoryCreate(); }
        }

        public IRequestOwnerRepository RequestOwnerRepository
        {
            get { return _repositoryfactory.RequestOwnerRepositoryCreate(); }
        }

        public ITroubleSubjectRepository TroubleSubjectRepository
        {
            get { return _repositoryfactory.TroubleSubjectRepositoryCreate(); }
        }

        public IDeviceRepository DeviceRepository
        {
            get { return _repositoryfactory.DeviceRepositoryCreate(); }
        }

        public IUserRepository UserRepository
        {
            get { return _repositoryfactory.UserRepositoryCreate(); }
        }

        public IMetroSearch MetroSearch
        {
            get { return _repositoryfactory.MetroSearchCreate(); }

        }
        public IDataFilter DataFilter
        {
            get { return _repositoryfactory.DataFilterCreate(); }
        }

        public IDataExport DataExport
        {
            get { return _repositoryfactory.DataExportCreate(); }
        }
        
        public IWidget Widget
        {
            get { return _repositoryfactory.WidgetCreate(); }
        }
    }
}