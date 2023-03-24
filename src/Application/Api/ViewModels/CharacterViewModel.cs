using Domain.Entities.Characters;

namespace Api.ViewModels;

/// <summary>
/// Class to provides a dynamic interaction with server through HATEOAS constraint
/// </summary>
/// <see href="https://en.wikipedia.org/wiki/HATEOAS"/>
public partial record CharacterViewModel
{
    public string Name { get; }
    public string JobClass { get; }
    public string Status { get; }
    public Dictionary<string, string> Links { get; }

    public CharacterViewModel(Character character)
    {
        Name = character.Name.Value;
        JobClass = character.JobClass.Name;
        Status = character.Status;
        Links = new()
        {
            ["details"] = $"/api/v1/character/{character.Name.Value}"
        };
    }
}

public partial record CharacterViewModel
{
    public static IEnumerable<CharacterViewModel> GetCharacterViewModel(IEnumerable<Character> characters) =>
        characters.Select(x => new CharacterViewModel(x));
}