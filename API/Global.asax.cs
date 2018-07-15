using Castle.Windsor;
using Castle.Windsor.Installer;
using CustomLog4netLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Routing;
using API.Attributes;
using API.Bootstrapper;
using API.TraceLogger;

namespace API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("type", "json", new MediaTypeHeaderValue("application/json")));

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("type", "xml", new MediaTypeHeaderValue("application/xml")));

            IWindsorContainer container = new WindsorContainer();

            container.Install(FromAssembly.This());

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(container));

            GlobalConfiguration.Configuration.Services.Replace(typeof(IExceptionHandler), new HandlerLogger(container.Resolve<ILogger>()));

            GlobalConfiguration.Configuration.Services.Replace(typeof(IExceptionLogger), new CustomExceptionLogger(container.Resolve<ILogger>()));

            GlobalConfiguration.Configuration.Filters.Add(new LoggingAttribute(container.Resolve<ILogger>()));
        }
    }
}
