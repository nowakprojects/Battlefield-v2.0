using Autofac;
using Battlefield.Infrastructure.EventHandlers;

namespace Battlefield.Infrastructure.IoC.Modules;

internal class EventModule : Module
{
    private readonly Func<IComponentContext, IEventDispatcher> _eventDispatcherProvider;

    public EventModule(Func<IComponentContext, IEventDispatcher> eventDispatcherProvider)
    {
        _eventDispatcherProvider = eventDispatcherProvider;
    }

    protected override void Load(ContainerBuilder builder)
    {
        var assembly = typeof(EventModule).Assembly;

        builder.RegisterAssemblyTypes(assembly)
            .AsClosedTypesOf(typeof(IEventHandler<>))
            .InstancePerLifetimeScope();

        builder.Register<IEventDispatcher>(c => _eventDispatcherProvider(c))
            .As<IEventDispatcher>()
            .InstancePerLifetimeScope();

        base.Load(builder);
    }
}

