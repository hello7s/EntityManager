﻿using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using EntityManager.Domain.Services;
using EntityManager.Infrastructure;

namespace EntityManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DependencyResolver.SetResolver(new NinjectDependencyResolver());
        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError().GetBaseException();

            var auditLog = new AzureWriter();

            auditLog.Error(ex);
        }
    }
}
