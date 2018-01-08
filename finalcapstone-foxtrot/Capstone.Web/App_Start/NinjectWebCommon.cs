[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Capstone.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Capstone.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Capstone.Web.App_Start
{
    using System;
    using System.Web;
    using Capstone.Web.DataAccess;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using System.Configuration;
    using Ninject;
    using Ninject.Web.Common;

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
            kernel.Bind<IUserSqlDAL>().To<UserSqlDAL>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["3bar_foxtrot"].ConnectionString);
            kernel.Bind<IPartnerSqlDAL>().To<PartnerSqlDAL>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["3bar_foxtrot"].ConnectionString);
            kernel.Bind<IExperimentSqlDAL>().To<ExperimentFormSqlDAL>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["3bar_foxtrot"].ConnectionString);
            kernel.Bind<IDataEntrySqlDAL>().To<DataEntrySqlDAL>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["3bar_foxtrot"].ConnectionString);
            kernel.Bind<IDataExplorationSqlDAL>().To<DataExplorationSqlDAL>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["3bar_foxtrot"].ConnectionString);
        }        
    }
}