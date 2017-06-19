using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMvvmLightAutofac.Model;

namespace WpfMvvmLightAutofac.Bootstrap
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataService>().As<IDataService>().InstancePerLifetimeScope();
        }
    }
}
