using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Domain.UseCases.Events;

namespace Domain.UseCases.Commands;

public partial record CreateNewCharacter
{
    [Required] public string SubscriptionKey { get; init; } = "";
    [Required] public string Name { get; init; } = "";
    [Required] public string JobClassName { get; init; } = "";
}

