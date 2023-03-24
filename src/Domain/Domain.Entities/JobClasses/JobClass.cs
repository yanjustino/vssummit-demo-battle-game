namespace Domain.Entities.JobClasses;

public record JobClass(string Name, JobClassAttributes Attributes)
{
    public JobClassModifiers Modifiers { get; private set; } = new ();

    public JobClass SetAttack(string label, Func<JobClass, decimal> func)
    {
        Modifiers = Modifiers with { Attack = (label, () => func.Invoke(this)) };
        return this;
    }

    public JobClass SetSpeed(string label, Func<JobClass, decimal> func)
    {
        Modifiers = Modifiers with { Speed = (label, () => func.Invoke(this)) };
        return this;
    }
    
    public int CalculatedSpeed() => Random.Shared.Next(0, (int)Modifiers.Speed.SpeedFunciont.Invoke());
    public int CalculatedAttack() => Random.Shared.Next(0, (int)Modifiers.Attack.AttackFunction.Invoke());
}