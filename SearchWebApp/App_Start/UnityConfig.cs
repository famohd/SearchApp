using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using SearchWebApp.Service;
using LogWrapper;

namespace SearchWebApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<ICoreLogger, CoreLogger>();
            container.RegisterType<ISearchApi, SearchApi>(new InjectionConstructor(new SearchApiClient().GetApiClient()));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}