using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace TradeNetter.DependencyRegistry
{
    public class AutofacRegistry : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Controller>()
                .As<IController>()
                .SingleInstance();

            builder.RegisterType<TradeProcessor>()
                .As<ITradeProcessor>()
                .SingleInstance();
        }
    }
}
