using Battlefield.Core.Domain;

namespace Battlefield.Infrastructure.Commands.BattleUnit
{
    public record CreateUnit(
        Guid BattleId,
        int X,
        int Y,
        string Type,
        Player Owner) : ICommand;
}
