using static UnitTests.Entities.Matches.MatchContext;

namespace UnitTests.Entities.Matches;

public class MatchTests
{
    [Fact]
    public void when_define_the_first_player()
    {
        GIVEN.a_match_with_two_player_with_same_job_class()
            .WHEN.define_the_first_player()
            .THEN.player_order_should_be_defined();
    }
}