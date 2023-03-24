using System.Collections.Concurrent;
using Domain.UseCases.Adapters.MessageBroker;

namespace Infrastructure.MessageBroker.EventDriven;

/// <summary>
/// This class is an implementation of Event-Driven Messaging pattern
/// </summary>
/// <see href="https://patterns.arcitura.com/soa-patterns/design_patterns/event_driven_messaging"/>
public class EventManager
{
    private ConcurrentDictionary<string, Queue<Envelope<IEvent>>> MessageState { get; }
    private ConcurrentDictionary<string, Action<string, object>> Subscribers { get; }

    public EventManager()
    {
        MessageState = new ConcurrentDictionary<string, Queue<Envelope<IEvent>>>();
        Subscribers = new ConcurrentDictionary<string, Action<string, object>>();
    }

    public async Task Produce<T>(T @event) where T : IEvent
    {
        var envelope = new Envelope<IEvent>(@event);
        if (!MessageState.ContainsKey(@event.Key))
            MessageState[@event.Key] =  new Queue<Envelope<IEvent>>();

        MessageState[@event.Key].Enqueue(envelope);
        await Task.Delay(5);
        await Notify(@event.Key);
    }

    protected async Task Notify(string key)
    {
        await Task.Yield();
        if (!MessageState.ContainsKey(key)) return;

        foreach (var message in MessageState[key].Select(_ => MessageState[key].Dequeue()))
        {
            Subscribers.TryGetValue(key, out var action);
            action?.Invoke(key, message);
        }
    }

    public async Task Subscribe(string key, Action<string, object> action)
    {
        await Task.Yield();
        Subscribers.TryAdd(key, action);        
    }
}