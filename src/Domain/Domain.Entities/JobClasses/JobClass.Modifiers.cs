using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities.JobClasses;

/// <summary>
/// CharacterName represents a ValueObject
/// </summary>
/// <see href="https://martinfowler.com/bliki/ValueObject.html"/>
public record JobClassModifiers
{
    public string AttackDescription => Attack.Label;
    [JsonIgnore]
    public (string Label, Func<decimal> AttackFunction) Attack { get; init; }

    public string SpeedDescription => Speed.Label;
    [JsonIgnore]
    public (string Label, Func<decimal> SpeedFunciont) Speed { get; init; }
}