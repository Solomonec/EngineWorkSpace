using MetroSupport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroSupport.BLL.Interfaces
{
    public interface IRepositoryFactory
    {
        ICallRequestRepository<ItCallRequest> ItCallRequestRepositoryCreate();
        ICallRequestRepository<AsppCallRequest> AsppCallRequestRepositoryCreate();
        ICallRequestRepository<SvyazCallRequest> SvyazCallRequestRepositoryCreate();
        ICallRequestRepository<PaCallRequest> PaCallRequestRepositoryCreate();
        ILocationRepository LocationRepositoryCreate();
        ICategoryRepository<ItCategory> ItCategoryRepositoryCreate();
        ICategoryRepository<AsppCategory> AsppCategoryRepositoryCreate();
        ICategoryRepository<SvyazCategory> SvyazCategoryRepositoryCreate(); 
        ICategoryRepository<PaCategory> PaCategoryRepositoryCreate();
        IDeviceModelRepository DeviceModelRepositoryCreate();
        IDepartmentRepository DepartmentRepositoryCreate();
        ITroubleSubjectRepository TroubleSubjectRepositoryCreate();
        IRequestOwnerRepository RequestOwnerRepositoryCreate();
        IBossRepository BossRepositoryCreate();
        IAssignerRepository AssignerRepositoryCreate();
        ICategoryIndexatorRepository CategoryIndexatorRepositoryCreate();
        IModelIndexatorRepository ModelIndexatorRepositoryCreate();
        IDeviceRepository DeviceRepositoryCreate();
        IUserRepository UserRepositoryCreate();
        IMetroSearch MetroSearchCreate();
        IDataFilter DataFilterCreate();
        IDataExport DataExportCreate();
        IWidget WidgetCreate();


    }
}