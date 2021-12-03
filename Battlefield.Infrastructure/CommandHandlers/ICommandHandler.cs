using Battlefield.Infrastructure.Commands;

namespace Battlefield.Infrastructure.CommandHandlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
