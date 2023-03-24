using Domain.UseCases.Adapters.Queries;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Storage;
using Microsoft.Extensions.DependencyInjection;
using Domain.UseCases.Adapters.Repositories;

namespace Infrastructure.Data;

public static class Injection
{
    public static IServiceCollection InjectDatabase(this IServiceCollection services)
    {
        services.AddSingleton<DataContext>();
        services.AddScoped<IJobClassRepository, JobClassRepository>();
        services.AddScoped<ICharacterRepository, CharacterRepository>();
        services.AddScoped<ICharacterQueryable, CharacterRepository>();
        return services;
    }
}