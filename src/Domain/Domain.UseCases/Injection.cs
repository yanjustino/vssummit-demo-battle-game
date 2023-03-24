using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Domain.UseCases.Adapters.MessageBroker;

namespace Domain.UseCases;

[ExcludeFromCodeCoverage]
public static class Injection
{
    public static IServiceCollection InjectUseCases(this IServiceCollection services)
    {
        services.AddScoped<OnCreateNewCharacter>();
        services.AddScoped<OnStartingNewMatch>();
        return services;
    }    
}