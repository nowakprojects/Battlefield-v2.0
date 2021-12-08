using Autofac;
using Battlefield.Infrastructure.Repositories;

namespace Battlefield.Infrastructure.IoC.Modules;
public class RepositoryModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<InMemoryBattlefieldRepository>()
           .As<IBattlefieldRepository>()
           .SingleInstance();
        
        builder.RegisterType<InMemoryGameEngineMemoryCache>()
            .As<IGameEnginesMemoryCache>()
            .SingleInstance();
    }
}
