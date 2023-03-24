namespace Domain.Entities.Matches;

public class MatchLog
{
    private readonly List<string> _values = new();

    public IEnumerable<string> Values => _values;

    public void LogInformation(string value) => _values.Add(value);
}