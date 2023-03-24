using Domain.Entities.Characters;
using Domain.Entities.JobClasses;
using FluentAssertions;

namespace UnitTests.Entities.Characters;

public static class CharacterFixture
{

    public static CharacterContext a_new_character(this CharacterContext context)
    {
        var character = new Character(nameof(Character), JobClassDefaultValues.GetDefaultMage());
        return context with { Character = character, HealthPointsBeforeAttack = character.JobClass.Attributes.HealthPoints };
    }

    public static CharacterContext a_character_is_killed(this CharacterContext context) 
    {
        context.Character.Kill();
        return context;
    }

    public static CharacterContext it_status_should_be(this CharacterContext context, CharacterStatus status)
    {
        context.Character.Status.Should().Be(status.ToString());
        return context;
    }
    
    public static CharacterContext job_class_is_not_null(this CharacterContext context)
    {
        context.Character.JobClass.Should().NotBeNull();
        return context;
    }   
    
    public static CharacterContext it_receives_an_attack_of(this CharacterContext context, int points)
    {
        context.Character.ReceiveAttack(points);
        return context;
    }     
    
    public static CharacterContext it_receives_an_attack_more_than_health_points(this CharacterContext context)
    {
        var healthPoints = context.Character.JobClass.Attributes.HealthPoints;
        context.Character.ReceiveAttack(healthPoints + 10);
        return context;
    }     
    
    public static CharacterContext health_points_should_be_less_than_before_attack(this CharacterContext context)
    {
        var healthPoints = context.Character.JobClass.Attributes.HealthPoints;
        healthPoints.Should().BeLessThan(context.HealthPointsBeforeAttack);
        return context;
    }     
    
    public static CharacterContext health_points_should_be_zero(this CharacterContext context)
    {
        var healthPoints = context.Character.JobClass.Attributes.HealthPoints;
        healthPoints.Should().Be(0);
        return context;
    }
}