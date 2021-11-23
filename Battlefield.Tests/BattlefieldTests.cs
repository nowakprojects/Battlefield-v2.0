using Moq;
using Xunit;
using Battlefield.Infrastructure.Repositories;
using Battlefield.Core.Domain;
using Battlefield.Infrastructure.EventHandlers.BattleUnit;
using Battlefield.Infrastructure.EventHandlers;
using Autofac;

namespace Battlefield.Tests;
public class BattlefieldTests
{
    [Fact]
    public async Task Test()
    {
        var battleRepository = new Mock<IBattlefieldRepository>();
        var battle = new Battle();
        await battleRepository.Object.AddAsync(battle);

        battleRepository.Verify(x => x.AddAsync(It.IsAny<Battle>()), Times.Once);
        
    }
    [Fact]
    public async Task EventHandlersShouldWork()
    {
        var battleRepository = new InMemoryBattlefieldRepository();
        var battle = new Battle();
        await battleRepository.AddAsync(battle);
        var unit = new BattleUnit();
        var @event = new UnitCreated(unit, battle.Id);

        var handler1 = new AddToBattlefieldCreatedUnit(battleRepository);
        var handler2 = new UpdateTileMapWhenUnitCreated(battleRepository);


        await handler1.HandleAsync(@event);
        await handler2.HandleAsync(@event);

        Assert.True(battle.Units.First().Id == unit.Id);
        Assert.True(battle.TileMap[0,0].Unit == unit);
    }
}
