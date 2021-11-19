using Battlefield.Infrastructure.Events;

namespace Battlefield.Infrastructure.EventHandlers
{
    internal interface IEventDispatcher
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
