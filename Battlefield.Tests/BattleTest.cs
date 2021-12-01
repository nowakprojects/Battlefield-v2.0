using Autofac;
using Battlefield.Infrastructure.Commands;
using Battlefield.Infrastructure.Commands.Battlefield;
using Battlefield.Infrastructure.EventHandlers;
using Battlefield.Infrastructure.IoC;

namespace Battlefield.Tests;
using Xunit;

public class BattleTest
{
    public class WhenCreateBattlefield
    {

        [Fact]
        public async Task ThenBattlefieldShouldBeCreatedIfNotExists()
        {
            // Given
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ContainerModule(c => new EventDispatcher(c)));
            var context = builder.Build();
            // var commandDispatcher = context.Resolve<ICommandDispatcher>();
            var eventDispatcher = context.Resolve<EventDispatcher>();
            //
            // // When
            // var createBattlefield = new CreateBattlefiled("name");
            // await commandDispatcher.DispatchAsync(createBattlefield);
            // // Then

        }
        
    }
    
}