using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using API.Business;

namespace API.Bootstrapper
{
    public class ProductInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IProductService>().ImplementedBy<ProductService>().LifeStyle.Singleton);
        }
    }
}