using Domain.Entities.Matches;
using Domain.UseCases.Events;

namespace Domain.UseCases.Commands;

public partial record StartNewMatch
{
    public MatchNotStarted ToCharacterNotFound(string name) =>
        new (SubscriptionKey, $"Character {name} Not Found");
    
    public MatchNotStarted ToCharacterNotAlive(string name) =>
        new (SubscriptionKey, $"Character {name} Not Alive");

    public MatchFinished ToMatchFinished(Match match) =>
        new(SubscriptionKey, "Match was finished", match.MatchLog);
}