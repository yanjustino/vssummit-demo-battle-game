using Domain.Entities.Characters;
using FluentAssertions;
using static UnitTests.Entities.Characters.CharacterContext;
using static UnitTests.Entities.Characters.CharacterFixture;

namespace UnitTests.Entities.Characters;

public class CharacterTests
{
    [Fact]
    public void When_character_is_created_it_status_should_be_alive()
    {
        GIVEN.a_new_character()
            .AND.job_class_is_not_null()
            .THEN.it_status_should_be(CharacterStatus.Alive);
    }    
    
    [Fact]
    public void When_character_is_killed_it_status_should_be_dead()
    {
        GIVEN.a_new_character()
            .AND.job_class_is_not_null()
            .WHEN.a_character_is_killed()
            .THEN.it_status_should_be(CharacterStatus.Dead);
    }

    [Fact]
    public void When_character_receive_an_attack()
    {
        GIVEN.a_new_character()
            .WHEN.it_receives_an_attack_of(10)
            .THEN.health_points_should_be_less_than_before_attack();
    }
    
    [Fact]
    public void When_character_should_be_dead_when_health_points_over()
    {
        GIVEN.a_new_character()
            .WHEN.it_receives_an_attack_more_than_health_points()
            .THEN.it_status_should_be(CharacterStatus.Dead)
            .AND.health_points_should_be_zero();
    }    
}