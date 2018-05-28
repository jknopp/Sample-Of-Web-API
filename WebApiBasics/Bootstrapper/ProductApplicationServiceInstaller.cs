using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WebApiBasics.Business;

namespace WebApiBasics.Bootstrapper
{
    public class ProductApplicationServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IProductApplicationService>().ImplementedBy<ProductApplicationService>().LifeStyle.Singleton);
        }
    }
}