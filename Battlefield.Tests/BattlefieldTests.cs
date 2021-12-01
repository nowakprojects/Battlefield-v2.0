using Moq;
using Xunit;
using Battlefield.Core.Domain;
using Battlefield.Infrastructure.Repositories;
using Battlefield.Core.Events.BattleUnit;
using Battlefield.Infrastructure.EventHandlers;
using Battlefield.Infrastructure.Commands.Battlefield;
using Battlefield.Infrastructure.Commands;
using Autofac;
using Battlefield.Core.Domain.Creatures;

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
    public async Task Update_tileMap_after_create_creature_should_be_correct()
    {
        var battleRepository = new InMemoryBattlefieldRepository();
        var battle = new Battle("name3");
        await battleRepository.AddAsync(battle);
        var @event = battle.CreateUnit(new Coordinates(4, 8), Player.BLUE, new Griffin());
        Assert.ThrowsAny<Exception>(() => @event = battle.CreateUnit(new Coordinates(4, 8), Player.RED, new Griffin()));
        Assert.NotNull(battle.Units.FirstOrDefault());
        Assert.True(battle.UnitOnTile(4,8) == battle.Units.FirstOrDefault());
    }
}
