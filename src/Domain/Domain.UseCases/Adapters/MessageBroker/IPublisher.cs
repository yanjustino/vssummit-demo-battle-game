namespace Domain.UseCases.Adapters.MessageBroker;

public interface IPublisher
{
    Task Notify(IEvent @event);
}