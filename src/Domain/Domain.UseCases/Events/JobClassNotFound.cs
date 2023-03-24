using Domain.UseCases.Adapters.MessageBroker;

namespace Domain.UseCases.Events;

public record JobClassNotFound(string Key, string Title, object? Result = null) : IEvent
{
}