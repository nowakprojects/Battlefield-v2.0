using Battlefield.Core.Domain;
using Battlefield.Infrastructure.CommandHandlers;
using Battlefield.Infrastructure.Commands.Battlefield;
using Battlefield.Infrastructure.Commands.BattleUnit;
using Battlefield.Infrastructure.EventHandlers;
using Battlefield.Infrastructure.Repositories;


namespace Battlefield.Infrastructure;
public class DataInitializer : IDataInitializer
{
    private readonly IBattlefieldRepository _battleRepo;
    private readonly ICommandDispatcher _dispatcher;

    public DataInitializer(IBattlefieldRepository battleRepo,
        ICommandDispatcher dispatcher)
    {
        _battleRepo = battleRepo;
        _dispatcher = dispatcher;
    }
    public async Task SeedAsync()
    {

        for (var i = 1; i <= 2; i++)
        {
            var command = new CreateBattlefiled($"name{i}");
            await _dispatcher.DispatchAsync(command);
            var battle = await _battleRepo.GetAsync($"name{i}");
            var command2 = new CreateUnit(battle.Id, 3,4,"Pikeman",Player.BLUE);
            var command3 = new CreateUnit(battle.Id, 5,7,"Archer",Player.RED);
            var command4 = new CreateUnit(battle.Id, 1,3,"Pikeman",Player.BLUE);
            await _dispatcher.DispatchAsync(command2);
            await _dispatcher.DispatchAsync(command3);
            await _dispatcher.DispatchAsync(command4);
        }
    }
}