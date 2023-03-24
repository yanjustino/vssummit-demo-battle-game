using Domain.Entities.Characters;
using Domain.UseCases.Adapters.Queries;
using Infrastructure.Data.Storage;
using Microsoft.Extensions.Logging;
using Domain.UseCases.Adapters.Repositories;

namespace Infrastructure.Data.Repositories;

public class CharacterRepository : ICharacterRepository, ICharacterQueryable
{
    public DataContext Context { get; }
    public ILogger<CharacterRepository> Logger { get; }

    public CharacterRepository(DataContext context, ILogger<CharacterRepository> logger)
    {
        Context = context;
        Logger = logger;
    }
    
    public async Task<(bool Fail, string Error)> Save(Character character)
    {
        await Task.Yield();
        if (Context.Characters.ContainsKey(character.Name.Value))
            return (true, $"The character {character.Name.Value} already registered");
        
        Context.Characters.TryAdd(character.Name.Value, character);
        Logger.LogInformation("New Character {name} was registered", character.Name.Value);
        return (false, string.Empty);
    }

    public async Task<Character?> Get(string name)
    {
        await Task.Yield();
        Context.Characters.TryGetValue(name, out var character);
        return character;
    }    

    public async Task<IEnumerable<Character>> GetAll()
    {
        await Task.Yield();
        return Context.Characters.IsEmpty 
            ? Array.Empty<Character>() 
            : Context.Characters.Values;
    }

    public async Task<IEnumerable<Character>> Get(int currentPage, int pageSize)
    {
        await Task.Yield();
        return Context.Characters.IsEmpty 
            ? Array.Empty<Character>() 
            : Context.Characters.Values.Skip(currentPage * pageSize).Take(pageSize);
    }
}