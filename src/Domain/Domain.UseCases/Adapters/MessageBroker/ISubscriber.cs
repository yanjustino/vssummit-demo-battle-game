namespace Domain.UseCases.Adapters.MessageBroker;

public interface ISubscriber
{
    Task<string> Subscribe(Action<string, object> action);
}