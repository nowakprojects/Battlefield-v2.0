using Autofac;
using Battlefield.Infrastructure.IoC.Modules;

namespace Battlefield.Infrastructure.IoC
{
    public class ContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<CommadModule>();
            builder.RegisterModule<EventModule>();

            base.Load(builder);
        }
    }
}
