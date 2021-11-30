using Autofac;
using Battlefield.Core.Events;

namespace Battlefield.Infrastructure.EventHandlers
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IComponentContext _context;

        public EventDispatcher(IComponentContext context)
        {

            _context = context;
        }

        public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var handlers = _context.Resolve<IEnumerable<IEventHandler<TEvent>>>();
            foreach(var handler in handlers)
                await handler.HandleAsync(@event);
        }
    }
}
