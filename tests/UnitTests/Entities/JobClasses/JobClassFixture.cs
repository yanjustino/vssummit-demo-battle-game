using Domain.Entities.JobClasses;
using FluentAssertions;

namespace UnitTests.Entities.JobClasses;

internal static class JobClassFixture
{
    public static JobClassContext a_warrior(this JobClassContext context) =>
        context with { JobClass = JobClassDefaultValues.GetDefaultWarrior() };

    public static JobClassContext its_attributes_are(this JobClassContext context, JobClassAttributes attributes) =>
        context with { JobClass = context.JobClass with{ Attributes = attributes } };

    public static JobClassContext its_attack_is(this JobClassContext context, Func<JobClass, decimal> func) =>
        context with { JobClass = context.JobClass.SetAttack("attack", func) };
    
    public static JobClassContext its_speed_is(this JobClassContext context, Func<JobClass, decimal> func) =>
        context with { JobClass = context.JobClass.SetSpeed("speed", func) };

    public static JobClassContext calculate_its_attack(this JobClassContext context) =>
        context with { Value = context.JobClass.Modifiers.Attack.AttackFunction.Invoke() };
    
    public static JobClassContext calculate_its_speed(this JobClassContext context) =>
        context with { Value = context.JobClass.Modifiers.Speed.SpeedFunciont.Invoke() };

    public static JobClassContext resul_should_be(this JobClassContext context, decimal value)
    {
        context.Value.Should().Be(value);
        return context;
    }

    public static JobClassContext the_attack_description_should_not_be_empty(this JobClassContext context)
    {
        context.JobClass.Modifiers.AttackDescription.Should().NotBeEmpty();
        return context;
    }

    public static JobClassContext the_speed_description_should_not_be_empty(this JobClassContext context)
    {
        context.JobClass.Modifiers.SpeedDescription.Should().NotBeEmpty();
        return context;
    }
}