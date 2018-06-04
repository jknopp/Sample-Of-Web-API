using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.DynamicProxy;

namespace WebApiBasics.Bootstrapper
{
    public class InterceptorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var y = Classes.FromThisAssembly().BasedOn<IInterceptor>();
            container.Register(
            Classes.FromAssemblyInDirectory(new AssemblyFilter(string.Empty))
                .BasedOn<IInterceptor>()
                .WithService.AllInterfaces()
                .LifestyleTransient());
        }
    }
}