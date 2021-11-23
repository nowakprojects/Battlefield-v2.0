using Battlefield.Core.Domain;

namespace Battlefield.Infrastructure.Commands.BattleUnit
{
    public record CreateUnit(
        Guid battleId,
        Coordinates Position,
        ICreature Type,
        Player Owner) : ICommand;
}
