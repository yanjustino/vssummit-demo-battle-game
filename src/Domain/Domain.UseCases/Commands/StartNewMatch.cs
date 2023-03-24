using System.ComponentModel.DataAnnotations;

namespace Domain.UseCases.Commands;

public partial record StartNewMatch
{
    [Required] public string SubscriptionKey { get; init; } = "";
    [Required] public string FirstPlayer { get; init; } = "";
    [Required] public string SecondPlayer { get; init; } = "";
}