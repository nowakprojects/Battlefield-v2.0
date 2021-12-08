using Autofac;
using Battlefield.Infrastructure.EventHandlers;

namespace Battlefield.Infrastructure.IoC.Modules;

internal class EventModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assembly = typeof(EventModule).Assembly;

        builder.RegisterAssemblyTypes(assembly)
            .AsClosedTypesOf(typeof(IEventHandler<>))
            .SingleInstance();

        builder.RegisterType<EventDispatcher>()
            .As<IEventDispatcher>()
            .SingleInstance();

        base.Load(builder);
    }
}

