using Microsoft.Extensions.Logging;
using Domain.UseCases.Adapters.MessageBroker;

namespace Infrastructure.MessageBroker.EventDriven;

/// <summary>
/// This class is an implementation of Pub/Sub Messaging
/// </summary>
/// <see href="https://aws.amazon.com/pub-sub-messaging/"/>
public class Subscriber : ISubscriber
{
    public ILogger<Publisher> Logger { get; }
    public EventManager Manager { get; }

    public Subscriber(ILogger<Publisher> logger, EventManager manager)
    {
        Logger = logger;
        Manager = manager;
    }
    
    public async Task<string> Subscribe(Action<string, object> action)
    {
        var key = Guid.NewGuid().ToString("N");
        await Manager.Subscribe(key, action);
        return key;
    }
}