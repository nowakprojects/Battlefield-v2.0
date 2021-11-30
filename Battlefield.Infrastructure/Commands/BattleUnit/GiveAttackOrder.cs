namespace Battlefield.Infrastructure.Commands.BattleUnit;

public record GiveAttackOrder(Guid BattleId, Guid UnitId, Guid TargetId) : ICommand;
