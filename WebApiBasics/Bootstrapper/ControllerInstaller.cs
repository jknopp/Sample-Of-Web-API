using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web.Http;

namespace WebApiBasics.Bootstrapper
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                        .BasedOn<ApiController>()
                        .LifestylePerWebRequest()
                        //.Configure(c=>c.Interceptors(typeof(LoggingInterceptor)))
                        );
        }
    }
}