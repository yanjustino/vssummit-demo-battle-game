using Domain.Entities;
using Domain.Entities.Characters;
using Domain.UseCases.Events;

namespace Domain.UseCases.Commands;

public partial record CreateNewCharacter
{
    public CharacterNotRegistered ToCharacterNotRegistered(string message) =>
        new (SubscriptionKey, message);
    
    public JobClassNotFound ToJobClassNotFound() =>
        new (SubscriptionKey, $"Job class '{JobClassName}' not found");

    public CharacterRegistered ToCharacterRegistered(Character character) =>
        new (SubscriptionKey, $"The character {character.Name} was created successfully!!!", character);    
}