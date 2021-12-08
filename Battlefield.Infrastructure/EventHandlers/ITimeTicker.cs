using Microsoft.Extensions.Hosting;

namespace Battlefield.Infrastructure.EventHandlers;

public interface ITimeTicker
{
    
    void OnTimeTick(Action callback);
    void Tick();
}
//
// class SystemTimeProvider : ITimeProvider
// {
//     private readonly int _tickMillis;
//
//     private List<Action> callbacks = new();
//
//     public SystemTimeProvider(int tickMillis)
//     {
//         _tickMillis = tickMillis;
//         Start();
//     }
//     
//     private void Start()
//     {
//         
//         var tickLoop = () =>
//         {
//             while (true)
//             {
//                 Tick();
//                 Thread.Sleep(_tickMillis);
//             }
//         };
//         var thread = new Thread(() => tickLoop);
//         thread.Start();
//     }
//     
//     private void Tick()
//     {
//         callbacks.ForEach(a => a.Invoke());
//     }
//     
//     public void OnTimeTick(Action callback)
//     {
//         callbacks.Add(callback);
//     }
// }

public class TimeTicker : ITimeTicker
{
    
    private List<Action> callbacks = new();
    
    public void Tick()
    {
        callbacks.ForEach(a => a.Invoke());
    }
    
    public void OnTimeTick(Action callback)
    {
        callbacks.Add(callback);
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
                Thread.Sleep(1000);
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