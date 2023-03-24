using Domain.Entities.JobClasses;

namespace Domain.UseCases.Adapters.Repositories;

public interface IJobClassRepository
{
    IEnumerable<JobClass> GetJobs();
    JobClass? GetByName(string name);
}