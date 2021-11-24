using Battlefield.Core.Events;

namespace Battlefield.Infrastructure.EventHandlers
{
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}
