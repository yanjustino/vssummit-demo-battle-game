using Microsoft.Extensions.Logging;
using Domain.UseCases.Adapters.MessageBroker;

namespace Infrastructure.MessageBroker.EventDriven;

/// <summary>
/// This class is an implementation of Pub/Sub Messaging
/// </summary>
/// <see href="https://aws.amazon.com/pub-sub-messaging/"/>
public class Publisher: IPublisher
{
    public ILogger<Publisher> Logger { get; }
    public EventManager Manager { get; }

    public Publisher(ILogger<Publisher> logger, EventManager manager)
    {
        Logger = logger;
        Manager = manager;
    }

    public async Task Notify(IEvent @event)
    {
        Logger.LogWarning(@event.Title);
        await Manager.Produce(@event);
    }
}