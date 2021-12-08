using Battlefield.Core.Domain;
using Battlefield.Core.Domain.Creatures;
using Battlefield.Core.Events;
using Battlefield.Core.Events.Battlefield;
using Battlefield.Core.Events.BattleUnit;
using FluentAssertions;

namespace Battlefield.Tests;
using Xunit;

public class BattleTest
{

    [Fact]
    void StartBattle()
    {
        // Given
        var battleId = Guid.NewGuid();   
        var battle = new Battle("name", battleId);
        
        // When
        var @event = battle.StartBattle();
        
        // Then
        @event.Should().Be(new BattleStarted(battleId, "name"));
    }
    
    [Fact]
    void LoadBattleStateFromEvents()
    {
        // Given
        var battleId = Guid.NewGuid();
        var unitId = Guid.NewGuid();
        var @events = new List<IEvent>
        {
            new BattleStarted(battleId, "name"),
            new UnitCreated(unitId, battleId, new Coordinates(1,1),"Archer", Player.RED)
        };
        
        // When
        var battle = Battle.Load(battleId, @events);
        
        // Then - tak raczej nie testujemy, bo chcemy skupic sie na zachowaniu, a nie jakie sa okreslone wartosci
        battle.Id.Should().Be(battleId);
        battle.Name.Should().Be("name");
        battle.Started.Should().BeTrue();
        battle.Units.ToList().Should().Contain(new BattleUnit(unitId, new Archer(), new Coordinates(1, 1), Player.RED));
    }
    
    [Fact]
    void WhenBattleIsStartedICannotStartTheBattle()
    {
        // Given
        var battleId = Guid.NewGuid();
        var @events = new List<IEvent>
        {
            new BattleStarted(battleId, "name")
        };
        
        // When
        var battle = Battle.Load(battleId, @events);
        Action act = () => battle.StartBattle();

        // Then - tak testujemy, ze jak wczesniej juz walke rozpoczelismy - w given eventy, to nie rozpoczniemy jej drugi raz
        act.Should().Throw<Exception>()
            .WithMessage("Battle is already started.");
    }
    
}