using Domain.Entities.Characters;

namespace Domain.UseCases.Adapters.Queries;

public interface ICharacterQueryable
{
    Task<Character?> Get(string name);
    Task<IEnumerable<Character>> GetAll();
    Task<IEnumerable<Character>> Get(int currentPage, int pageSize);
}