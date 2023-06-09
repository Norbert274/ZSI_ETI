[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(nclprospekt.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(nclprospekt.App_Start.NinjectWebCommon), "Stop")]

namespace nclprospekt.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using nclprospekt.Repository;
    using nclprospekt.Utils;
    using System.Web.Http;

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

                // Install our Ninject-based IDependencyResolver into the Web API config
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

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
            kernel.Bind<IZamowienieRepository>().To<ZamowienieRepository>();
            kernel.Bind<IStanyRepository>().To<StanyRepository>();
            kernel.Bind<IUzytkownikRepository>().To<UzytkownikRepository>();
            kernel.Bind<IAdresRepository>().To<AdresRepository>();
            kernel.Bind<IDashboardRepository>().To<DashboardRepository>();
            kernel.Bind<IAwizoRepository>().To<AwizoRepository>();
			kernel.Bind<IPomocRepository>().To<PomocRepository>();
            kernel.Bind<IAdresaciRepository>().To<AdresaciRepository>();
        }        
    }
}
