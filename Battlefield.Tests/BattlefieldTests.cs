using Moq;
using Xunit;
using Battlefield.Core.Domain;
using Battlefield.Infrastructure.Repositories;
using Battlefield.Core.Events.BattleUnit;
using Battlefield.Infrastructure.EventHandlers;

namespace Battlefield.Tests;
public class BattlefieldTests
{
    [Fact]
    public async Task Test()
    {
        var battleRepository = new InMemoryBattlefieldRepository();
        var battle = new Battle();
        await battleRepository.AddAsync(battle);

        Assert.True(await battleRepository.GetAsync(battle.Id) == battle);
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
        await Task.CompletedTask;
        Assert.True(battle.Units.First().Id == unit.Id);
        Assert.True(battle.TileMap[0,0].Unit == unit);
    }
}
