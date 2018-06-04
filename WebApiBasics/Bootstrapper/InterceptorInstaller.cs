using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.DynamicProxy;
using WebApiBasics.Interceptors;

namespace WebApiBasics.Bootstrapper
{
    public class InterceptorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<LoggingInterceptor>().LifeStyle.Singleton);
        }
    }
}