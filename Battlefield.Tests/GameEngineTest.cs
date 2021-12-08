using Battlefield.Core.Domain;
using Battlefield.Core.Events;
using Battlefield.Core.Events.Battlefield;
using Battlefield.Infrastructure.AI;
using Battlefield.Infrastructure.EventHandlers;
using FluentAssertions;

namespace Battlefield.Tests;
using Xunit;

public class GameEngineTest
{

    [Fact]
    public void Test1()
    {
        // Given
        var battleId = Guid.NewGuid();
        var @events = new List<IEvent>
        {
            new BattleStarted(battleId, "name")
        };
        var battle = Battle.Load(battleId, @events);
        
        var ticker = new TimeTicker();
        var engine = new GameEngine(ticker, battle);
        engine.TickCount.Should().Be(0);
        
        // When
        ticker.Tick();
        ticker.Tick();

        // Then
        engine.TickCount.Should().Be(2);
    }
    
    
    
}