using Battlefield.Infrastructure.Commands;

namespace Battlefield.Infrastructure.CommandHandlers
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}
