using Domain.Entities;
using Domain.Entities.Characters;
using Domain.UseCases.Adapters.MessageBroker;

namespace Domain.UseCases.Events;

public record CharacterRegistered (string Key, string Title, object? Result = null): IEvent
{
    
}