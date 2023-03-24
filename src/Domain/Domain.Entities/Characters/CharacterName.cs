using System.Text.RegularExpressions;

namespace Domain.Entities.Characters;

/// <summary>
/// CharacterName represents a ValueObject
/// </summary>
/// <see href="https://martinfowler.com/bliki/ValueObject.html"/>
public record CharacterName
{
    const int MAX_LENGHT = 15;

    public string Value { get; private init; } = "";

    public static CharacterName Create(string value)
    {
        ThrowIfInvalidInput(value);
        ThrowIfInvalidInputLength(value);
        return new CharacterName { Value = value };
    }

    private static void ThrowIfInvalidInput(string value)
    {
        var isInvalidInput = Regex.Match(value, "(\\d+)|\\W+").Success;
        if (isInvalidInput) throw new EntityArgumentException($"{value} Is an invalid character name");
    }
    
    private static void ThrowIfInvalidInputLength(string value)
    {
        var isInvalidInputSize = value.Length > MAX_LENGHT;
        if (isInvalidInputSize) throw new EntityArgumentException($"The character has an invalid name length");
    }
}