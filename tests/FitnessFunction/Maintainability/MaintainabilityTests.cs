using Api.Controllers;
using ArchUnitNET.Domain.Extensions;
using ArchUnitNET.xUnit;

namespace FitnessFunction.Maintainability;

public class MaintainabilityTests: IClassFixture<MaintainabilityFixture>
{
    public MaintainabilityFixture Fixture { get; }

    public MaintainabilityTests(MaintainabilityFixture fixture) => Fixture = fixture;

    [Fact]
    public void Event_classes_should_reside_in_domain_use_cases_events()
    {
        var rule = Types().That()
            .Are(Fixture.EventClasses)
            .Should().ResideInNamespace(Fixture.EventNamespace);
        
        rule.Check(Fixture.Architecture);
    } 
    
    [Fact]
    public void Event_classes_should_not_contains_event_in_the_end_of_name()
    {
        var rule = Types().That()
            .Are(Fixture.EventClasses)
            .Should().NotHaveNameEndingWith("Event");
        
        rule.Check(Fixture.Architecture);
    }     
    
    [Fact]
    public void Repositories_should_be_assignable_to_repository_adapter()
    {
        var rule = Types().That()
            .Are(Fixture.RepositoriesClasses)
            .Should().BeAssignableTo(Fixture.AdapterRepository);
        
        rule.Check(Fixture.Architecture);
    }

    [Fact]
    public void Api_controllers_should_not_access_repositories()
    {
        var rule = Types().That()
            .Are(Fixture.ApiControllers)
            .Should().NotDependOnAny(Fixture.AdapterRepository);
        
        rule.Check(Fixture.Architecture);        
    }
    
    [Fact]
    public void Api_controllers_should_have_a_field_with_name_that_ends_with_query()
    {
        var rule = Types().That()
            .Are(Fixture.ApiControllers).And()
            .DependOnAny(Fixture.AdapterQueries)
            .Should().FollowCustomCondition(f => 
                f.GetFieldMembers().Any(x => x.NameEndsWith("Query") && x.Visibility == Visibility.Private), "fields", "query field not found");

        rule.Check(Fixture.Architecture);        
    }
}