namespace Battlefield.Infrastructure.Commands.Battlefield;
public class StartBattle : ICommand
{
    public Guid BattleId { get; set; }
}
