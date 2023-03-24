namespace Domain.UseCases.Adapters.MessageBroker;

public interface IEvent
{
    string Key { get; }
    string Title { get; }
    object? Result { get; }
}