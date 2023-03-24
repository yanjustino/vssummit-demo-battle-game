using Domain.Entities.Characters;
using Domain.Entities.JobClasses;

namespace UnitTests.Entities.Characters;

public record CharacterContext
{
    public static CharacterContext GIVEN => new ();
    public CharacterContext WHEN => this;
    public CharacterContext THEN => this;
    public CharacterContext AND => this;

    public Character Character { get; init; } = new Character("warrior", JobClassDefaultValues.GetDefaultWarrior());
    public int HealthPointsBeforeAttack { get; init; }
}