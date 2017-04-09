using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using SearchWebApp.Service;

namespace SearchWebApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ISearchApi, SearchApi>(new InjectionConstructor(new SearchApiClient().GetApiClient()));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}