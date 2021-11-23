using Autofac;
using Battlefield.Infrastructure.Events;

namespace Battlefield.Infrastructure.EventHandlers
{
    public sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IComponentContext _context;

        public EventDispatcher(IComponentContext context)
            => _context = context;

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var handler = _context.Resolve<IEventHandler<TEvent>>();
            await handler.HandleAsync(@event);
        }
    }
}
