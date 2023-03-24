using Domain.Entities.JobClasses;
using Infrastructure.Data.Storage;
using Domain.UseCases.Adapters.Repositories;

namespace Infrastructure.Data.Repositories;

public class JobClassRepository : IJobClassRepository
{
    public DataContext Context { get; }

    public JobClassRepository(DataContext context)
    {
        Context = context;
    }
    
    public IEnumerable<JobClass> GetJobs() =>
        Context.JobClasses.Values.Any() ? Context.JobClasses.Values : Array.Empty<JobClass>();

    public JobClass? GetByName(string name) =>
        Context.JobClasses.TryGetValue(name, out var jobClass) ? jobClass : null;
}