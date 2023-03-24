using Domain.Entities.JobClasses;

namespace Domain.Entities.Characters;

public class Character
{
    public CharacterName Name { get; }
    public string Status { get; private set; }
    public JobClass JobClass { get; private set; }

    public Character(string name, JobClass jobClass)
    {
        Name = CharacterName.Create(name);
        Status = CharacterStatus.Alive.ToString();
        JobClass = jobClass;
    }

    public bool IsAlive() => Status == CharacterStatus.Alive.ToString() && JobClass.Attributes.HealthPoints > 0;
    public bool IsDead() => !IsAlive();

    public Character ReceiveAttack(int points)
    {
        var result = JobClass.Attributes.HealthPoints - points;
        if (result <= 0) return Kill();
        
        var attributes = JobClass.Attributes with { HealthPoints = result };
        JobClass = JobClass with { Attributes = attributes };
        return this;
    }

    public Character Kill()
    {
        Status = CharacterStatus.Dead.ToString();
        var attributes = JobClass.Attributes with { HealthPoints = 0 };
        JobClass = JobClass with { Attributes = attributes };
            
        return this;
    }
}