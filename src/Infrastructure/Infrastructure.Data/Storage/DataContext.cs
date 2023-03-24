using System.Collections.Concurrent;
using Domain.Entities.Characters;
using Domain.Entities.JobClasses;

namespace Infrastructure.Data.Storage;

/// <summary>
/// Thinking in high availability, this class use a Dictionary which is like a hash table
/// and ConcurrentDictionary which works with thread-safe approach.
/// Both can retrieve values close to O(1), in the best scenarios, or O(log n) and O(n). 
/// </summary>
public class DataContext
{
    public ConcurrentDictionary<string, Character> Characters { get; }
    public ConcurrentDictionary<string, JobClass> JobClasses { get; }

    public DataContext()
    {
        Characters = new ConcurrentDictionary<string, Character>();
        JobClasses = JobClassDefaultValues.ToDictionary();
    }
}