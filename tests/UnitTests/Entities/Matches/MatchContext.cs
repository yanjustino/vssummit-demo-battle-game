using Domain.Entities.Matches;

namespace UnitTests.Entities.Matches;

public record MatchContext
{
    public static MatchContext GIVEN => new ();
    public MatchContext WHEN => this;
    public MatchContext THEN => this;
    public MatchContext AND => this;

    public Match Match { get; set; }
}