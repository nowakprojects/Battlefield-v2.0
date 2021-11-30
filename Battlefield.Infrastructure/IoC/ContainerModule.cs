using Autofac;
using Battlefield.Infrastructure.IoC.Modules;
using Battlefield.Infrastructure.Mappers;

namespace Battlefield.Infrastructure.IoC
{
    public class ContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AtuoMapperConfig.Initializate())
                .SingleInstance();
            builder.RegisterModule<EventModule>();
            builder.RegisterModule<CommadModule>();
            builder.RegisterModule<RepositoryModule>();

            builder.RegisterModule<DataInitializerModule>();
            base.Load(builder);
        }
    }
}
