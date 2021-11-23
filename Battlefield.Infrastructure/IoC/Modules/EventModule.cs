using Autofac;
using Battlefield.Infrastructure.EventHandlers;

namespace Battlefield.Infrastructure.IoC.Modules
{
    internal class EventModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ContainerModule).Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IEventHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<EventDispatcher>()
                .As<IEventDispatcher>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
