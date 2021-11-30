using Autofac;

namespace Battlefield.Infrastructure.IoC.Modules;
internal class DataInitializerModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DataInitializer>()
                .As<IDataInitializer>()
                .InstancePerLifetimeScope();

        base.Load(builder);
    }
}
