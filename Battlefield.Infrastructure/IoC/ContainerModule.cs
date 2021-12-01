using Autofac;
using Battlefield.Infrastructure.EventHandlers;
using Battlefield.Infrastructure.IoC.Modules;
using Battlefield.Infrastructure.Mappers;

namespace Battlefield.Infrastructure.IoC
{
    public class ContainerModule : Module
    {
        private readonly Func<IComponentContext, IEventDispatcher> _eventDispatcherProvider;

        public ContainerModule(Func<IComponentContext, IEventDispatcher> eventDispatcherProvider)
        {
            _eventDispatcherProvider = eventDispatcherProvider;
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AtuoMapperConfig.Initializate())
                .SingleInstance();
            builder.RegisterModule(new EventModule(_eventDispatcherProvider));
            builder.RegisterModule<CommadModule>();
            builder.RegisterModule<RepositoryModule>();

            builder.RegisterModule<DataInitializerModule>();
            base.Load(builder);
        }
    }
}
