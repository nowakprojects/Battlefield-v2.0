namespace Battlefield.Core.Events.BattleUnit;
public record UnitDeleted(Domain.BattleUnit UnitToRemove) :IEvent;

