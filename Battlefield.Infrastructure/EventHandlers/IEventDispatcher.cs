﻿using Battlefield.Core.Events;

namespace Battlefield.Infrastructure.EventHandlers
{
    public interface IEventDispatcher
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
