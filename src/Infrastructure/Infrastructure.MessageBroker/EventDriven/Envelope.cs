using Domain.UseCases.Adapters.MessageBroker;

namespace Infrastructure.MessageBroker.EventDriven;

/// <summary>
/// The Envelope Class is a implementation of Message Metadata Pattern.
/// Also provides bases to satisfy Service Message pattern
/// </summary>
/// <see href="https://patterns.arcitura.com/soa-patterns/design_patterns/messaging_metadata"/>
/// <see href="https://patterns.arcitura.com/soa-patterns/design_patterns/service_messaging"/>
/// <param name="Data">activity-specific data</param>
/// <typeparam name="T"></typeparam>
public record Envelope<T>(T Data) where T : IEvent
{
    public DateTime OccurredOn { get; } = DateTime.Now;
    public string EventName { get; } = Data.GetType().Name;
}