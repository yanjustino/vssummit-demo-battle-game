using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities.JobClasses;

[ExcludeFromCodeCoverage]
public static class JobClassDefaultValues
{
    public const string Warrior = "Warrior";
    public const string Thief = "Thief";
    public const string Mage = "Mage";

    public static ConcurrentDictionary<string, JobClass> ToDictionary()
    {
        var warrior = GetDefaultWarrior();
        var thief = GetDefaultThief();
        var mage = GetDefaultMage();

        return new ConcurrentDictionary<string, JobClass>()
        {
            [Warrior] = warrior,
            [Thief] = thief,
            [Mage] = mage
        };
    }

    public static JobClass GetDefaultWarrior() =>
        new JobClass(Warrior, new JobClassAttributes(20, 10, 5, 5))
            .SetAttack(
                "Ataque: 80% da Força + 20% da Destreza",
                job => job.Attributes.Strength * 0.8m + job.Attributes.Dexterity * 0.2m)
            .SetSpeed(
                "Velocidade: 60% da Destreza + 20% da Inteligência",
                job => job.Attributes.Dexterity * 0.6m + job.Attributes.Intelligence * 0.2m);

    public static JobClass GetDefaultThief() =>
        new JobClass(Thief, new JobClassAttributes(15, 4, 10, 4))
            .SetAttack(
                "Ataque: 25% da Força + 100% da Destreza + 25% da Inteligência",
                job => job.Attributes.Strength * 0.25m + job.Attributes.Dexterity * 0.25m)
            .SetSpeed(
                "Velocidade: 80% da Destreza",
                job => job.Attributes.Dexterity * 0.8m);

    public static JobClass GetDefaultMage() =>
        new JobClass(Mage, new JobClassAttributes(12, 5, 6, 10))
            .SetAttack(
                "Ataque: 20% da Força + 50% da Destreza + 150% da Inteligência",
                job => job.Attributes.Strength * 0.2m + job.Attributes.Dexterity * 0.5m + job.Attributes.Intelligence * 1.5m )
            .SetSpeed(
                "Velocidade: 20% da Força + 50% da Destreza",
                job => job.Attributes.Strength * 0.2m + job.Attributes.Dexterity * 0.5m);        
}