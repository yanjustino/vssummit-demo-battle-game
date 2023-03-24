using Domain.Entities.JobClasses;

namespace UnitTests.Entities.JobClasses;

internal record JobClassContext
{
    public static JobClassContext GIVEN => new ();
    public JobClassContext WHEN => this;
    public JobClassContext THEN => this;
    public JobClassContext AND => this;

    public JobClass JobClass { get; init; } = JobClassDefaultValues.GetDefaultWarrior();
    public decimal Value { get; init; }
}