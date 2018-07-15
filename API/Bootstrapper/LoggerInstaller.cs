using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CustomLog4netLibrary;
using System.IO;

namespace API.Bootstrapper
{
    public class LoggerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ILogger>().ImplementedBy<Log4NetLogger>().LifeStyle.Singleton);
            var logfile = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
            Log4NetLogger.Init(logfile);
        }
    }
}