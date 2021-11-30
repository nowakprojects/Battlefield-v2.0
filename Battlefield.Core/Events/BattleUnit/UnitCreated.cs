namespace Battlefield.Core.Events.BattleUnit;
public record UnitCreated(Domain.BattleUnit Unit, Guid BattleId) : IEvent;
