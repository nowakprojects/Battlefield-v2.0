using Moq;
using Xunit;
using Battlefield.Core.Domain;
using Battlefield.Infrastructure.Repositories;
using Battlefield.Core.Events.BattleUnit;
using Battlefield.Infrastructure.EventHandlers;
using Battlefield.Infrastructure.Commands.Battlefield;
using Battlefield.Infrastructure.Commands;
using Autofac;

namespace Battlefield.Tests;
public class BattlefieldTests
{
    private readonly IBattlefieldRepository _battlefieldRepository;

    public BattlefieldTests()
    {
        _battlefieldRepository = new InMemoryBattlefieldRepository();
    }

    [Fact]
    public async Task Commad_dispatcher_should_invoke_resolve_method()
    {
        var context = new Mock<IComponentContext>();
        var dispatcher = new CommandDispatcher(context.Object);
        var commad = new CreateBattlefiled("name");
        await dispatcher.DispatchAsync<CreateBattlefiled>(commad);
        context.Verify(context => context.Resolve<CreateBattlefieldHandler>(), Times.Once);
        
    }
    [Fact]
    public async Task EventHandlersShouldWork()
    {
        var battleRepository = new InMemoryBattlefieldRepository();
        var battle = new Battle("name3");
        await battleRepository.AddAsync(battle);
        var unit = new BattleUnit();
        var @event = new UnitCreated(unit, battle.Id);

        var handler1 = new AddToBattlefieldCreatedUnit(battleRepository);
        var handler2 = new UpdateTileMapWhenUnitCreated(battleRepository);


        await handler1.HandleAsync(@event);
        await handler2.HandleAsync(@event);
        await Task.CompletedTask;
        Assert.True(battle.Units.First().Id == unit.Id);
        Assert.True(battle.TileMap[0,0].Unit == unit);
    }
}
