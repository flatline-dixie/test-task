using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using TestTask.Common.DAL;
using TestTask.Shortener.DAL.Entities;
using TestTask.Shortener.BE;
using TestTask.Shortener.BL.Mapper;
using TestTask.Shortener.BL;

namespace TestTask.Shortener.Configuration
{
    public class UnityConfiguration
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer Initialise(IUnityContainer container)
        {
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);

            return container;
        }

        public static void RegisterUnityFilterExtension(IUnityContainer container)
        {
            IFilterProvider filterProvider = FilterProviders.Providers.Single(p => p is FilterAttributeFilterProvider);
            FilterProviders.Providers.Remove(filterProvider);

            var unityFilterAttributeFilterProvider = new UnityFilterAttributeFilterProvider(container);
            FilterProviders.Providers.Add(unityFilterAttributeFilterProvider);
        }

        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {

            container
                .RegisterType(typeof(ICommonRepository<,,>), typeof(CommonRepository<,,>), new PerRequestLifetimeManager())
                .RegisterType<IMapper<User, ShortenerUser>, UserMapper>(new PerRequestLifetimeManager())
                .RegisterType<IMapper<UserLink, ShortenerUserLink>, ShortenerLinkMapper>(new PerRequestLifetimeManager())
                .RegisterType<IShortenerBL, ShortenerBL>(new PerRequestLifetimeManager());
        }

    }
}
