using Domain.Entities.Characters;
using Domain.Entities.JobClasses;
using FluentAssertions;
using Domain.Entities.Matches;

namespace UnitTests.Entities.Matches;

public static class MatchFixture
{
    public static MatchContext a_match_with_two_player_with_same_job_class(this MatchContext context)
    {
        var match = new Match(
            new Character("player_a", JobClassDefaultValues.GetDefaultMage()),
            new Character("player_b", JobClassDefaultValues.GetDefaultMage())
        );

        return context with { Match = match };
    }

    public static MatchContext define_the_first_player(this MatchContext context)
    {
        context.Match.DefineFirstPlayer();
        return context;
    }
    
    public static MatchContext player_order_should_be_defined(this MatchContext context)
    {
        var playerOrder = context.Match.PlayersOrder;
        playerOrder.Any().Should().BeTrue();
        return context;
    }
}