namespace Battlefield.Core.Events.BattleUnit;
public record UnitCreated(Guid UnitId, Guid BattleId) : IEvent;
