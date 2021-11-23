using Autofac;
using Battlefield.Infrastructure.Commands;

namespace Battlefield.Infrastructure.IoC.Modules
{
    public class CommadModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ContainerModule).Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
