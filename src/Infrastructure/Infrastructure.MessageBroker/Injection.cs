using Infrastructure.MessageBroker.EventDriven;
using Microsoft.Extensions.DependencyInjection;
using Domain.UseCases.Adapters.MessageBroker;

namespace Infrastructure.MessageBroker;

public static class Injection
{
    public static IServiceCollection InjectMsBroker(this IServiceCollection services)
    {
        services.AddSingleton<MessagingQueue>();
        services.AddSingleton<EventManager>();
        services.AddScoped<IPublisher, Publisher>();
        services.AddScoped<ISubscriber, Subscriber>();
        return services;
    }
}