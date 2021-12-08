namespace Battlefield.Core.Events.Battlefield;
public record BattleStarted(Guid BattleId, string Name) : IEvent;

