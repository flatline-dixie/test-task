using Microsoft.Practices.Unity;
using System.Linq;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TestTask.Shortener.Configuration;
using TestTask.Shortener.DAL;

using MirationConfiguration = TestTask.Shortener.DAL.Migrations.Configuration;

namespace Shortener.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = UnityConfiguration.GetConfiguredContainer();
            UnityConfiguration.Initialise(BuildWebAppDependencies(container));
            UnityConfiguration.RegisterUnityFilterExtension(container);

            InitData(container);
        }

        private static IUnityContainer BuildWebAppDependencies(IUnityContainer container)
        {
            return container;
        }

        private static void InitData(IUnityContainer container)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<ShortenerContext, 
                MirationConfiguration>()
            );

            var configuration = new MirationConfiguration();
            var migrator = new System.Data.Entity.Migrations.DbMigrator(configuration);
            if (migrator.GetPendingMigrations().Any())
            {
                migrator.Update();
            }
        }
    }
}
