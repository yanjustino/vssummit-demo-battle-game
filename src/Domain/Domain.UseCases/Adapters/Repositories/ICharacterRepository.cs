using Domain.Entities.Characters;

namespace Domain.UseCases.Adapters.Repositories;

public interface ICharacterRepository
{
    Task<(bool Fail, string Error)> Save(Character character);
}