using Battlefield.Core.Events;
using Microsoft.Extensions.Hosting;

namespace Battlefield.Infrastructure.EventHandlers;

public interface ITimeTicker
{
    
    void OnTimeTick(Func<IEnumerable<IEvent>> callback);
    void Tick();
}


public class TimeTicker : ITimeTicker
{
    
    private readonly List<Func<IEnumerable<IEvent>>> _callbacks = new();
    
    public void Tick()
    {
        _callbacks.ForEach(a => a.Invoke());
    }
    
    public void OnTimeTick(Func<IEnumerable<IEvent>> callback)
    {
        _callbacks.Add(callback);
    }
}

// todo: sprobowac zrozumiec
public class EventDispatcherTimeTicker : ITimeTicker
{
    private readonly ITimeTicker _timeTicker;
    private readonly IEventDispatcher _eventDispatcher;

    public EventDispatcherTimeTicker(ITimeTicker timeTicker, IEventDispatcher eventDispatcher)
    {
        _timeTicker = timeTicker;
        _eventDispatcher = eventDispatcher;
    }

    public void Tick()
    {
        _timeTicker.Tick();
    }

    public void OnTimeTick(Func<IEnumerable<IEvent>> callback)
    {
        _timeTicker.OnTimeTick(() =>
        {
            var tickEvents = callback().ToList();
            foreach (var tickEvent in tickEvents)
            {
                _eventDispatcher.PublishAsync(tickEvent);
            }

            return tickEvents;
        });
    }
}

// todo: cancel gracefully
public class TimeTickBackgroundService : IHostedService
{
    private readonly ITimeTicker _timeTicker;
    private bool _ticking = true;

    public TimeTickBackgroundService(ITimeTicker timeTicker)
    {
        _timeTicker = timeTicker;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        new Thread(() =>
        {
            while (_ticking)
            {
                Thread.Sleep(200);
                _timeTicker.Tick();
            }
        }).Start();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _ticking = false;
        //cancellationToken.ThrowIfCancellationRequested();
        return Task.CompletedTask;
    }
}