using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using DddCqrsEsExample.Web2.Infrastructure;
using Inforigami.Regalo.Core;

namespace DddCqrsEsExample.Web2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BootstrapContainer();

            var logger = container.Resolve<ILogger>();
            logger.Debug(this, "Application Start");
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var logger = container.Resolve<ILogger>();
            var request = HttpContext.Current.Request;
            logger.Debug(this, $"Application BeginRequest {request.HttpMethod} {request.Path}");
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var logger = container.Resolve<ILogger>();
            Exception exception = Server.GetLastError();
            var request = HttpContext.Current.Request;
            logger.Error(this, exception, $"Application Error {request.HttpMethod} {request.Path}");
        }

        protected void Application_End()
        {
            var logger = container.Resolve<ILogger>();
            logger.Debug(this, "Application End");

            container.Dispose();
        }

        private static void BootstrapContainer()
        {
            container = new WindsorContainer().Install(FromAssembly.This());
            container.Register(Component.For<IWindsorContainer>().Instance(container));
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}
