using DeviceBase.Models;
using MetroSupport.BLL;
using MetroSupport.BLL.Implement;
using MetroSupport.Commons;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MetroSupport.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MetroSupport.App_Start.NinjectWebCommon), "Stop")]

namespace MetroSupport.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Extensions.Factory;
    using Ninject.Extensions.Interception;
    using MetroSupport.BLL.Implements;
    using MetroSupport.BLL.Interfaces;
    using MetroSupport.Models;
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ICallRequestRepository<ItCallRequest>>().To<ItCallRequestRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<ICallRequestRepository<SvyazCallRequest>>().To<SvyazCallRequestRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<ICallRequestRepository<AsppCallRequest>>().To<AsppCallRequestRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<ICallRequestRepository<PaCallRequest>>().To<PaCallRequestRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<ILogCallRequestRepository<ItCallRequestLog>>().To<ItCallRequestLogRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<ILogCallRequestRepository<SvyazCallRequestLog>>().To<SvyazCallRequestLogRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<ILogCallRequestRepository<PaCallRequestLog>>().To<PaCallRequestLogRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<ILogCallRequestRepository<AsppCallRequestLog>>().To<AsppCallRequestLogRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<ILocationRepository>().To<LocationRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<ICategoryRepository<ItCategory>>().To<ItCategoryRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<ICategoryRepository<SvyazCategory>>().To<SvyazCategoryRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<ICategoryRepository<AsppCategory>>().To<AsppCategoryRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<ICategoryRepository<PaCategory>>().To<PaCategoryRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<ITroubleSubjectRepository>().To<TroubleSubjectRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<IDepartmentRepository>().To<DepartmentRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<IDeviceModelRepository>().To<DeviceModelRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<ICategoryIndexatorRepository>().To<CategoryIndexatorRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<IModelIndexatorRepository>().To<ModelIndexatorRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<IAssignerRepository>().To<AssignerRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<IRequestOwnerRepository>().To<RequestOwnerRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<IBossRepository>().To<BossRepository>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<IMetroSearch>().To<MetroSearch>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<IUserRepository>().To<UserRepository>().WithConstructorArgument("context", new UsersContext());
            kernel.Bind<IDeviceRepository>().To<DeviceRepository>().WithConstructorArgument("context", new DeviceContext());
            kernel.Bind<IDataFilter>().To<DataFilter>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<IWidget>().To<Widget>().WithConstructorArgument("metro", new MetroSupportContext());
            kernel.Bind<IDataExport>().To<DataExport>();
            
            kernel.Bind<IRepositoryFactory>().ToFactory();
        }        
    }
}
