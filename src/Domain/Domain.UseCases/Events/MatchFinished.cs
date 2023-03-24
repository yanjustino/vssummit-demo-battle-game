using Domain.Entities.Matches;
using Domain.UseCases.Adapters.MessageBroker;

namespace Domain.UseCases.Events;

public record MatchFinished(string Key, string Title, object? Result = null) : IEvent;
