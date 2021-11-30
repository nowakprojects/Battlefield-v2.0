using Battlefield.Infrastructure.Commands;

namespace Battlefield.Infrastructure.Commands.BattleUnit;
public class DeleteUnit : ICommand
{
    public Guid UnitId { get; set; }
}
