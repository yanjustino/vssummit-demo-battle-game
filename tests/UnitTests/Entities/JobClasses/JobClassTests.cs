using Domain.Entities.JobClasses;
using static UnitTests.Entities.JobClasses.JobClassContext;
using static UnitTests.Entities.JobClasses.JobClassFixture;

namespace UnitTests.Entities.JobClasses;

public class JobClassTests
{
    [Fact]
    public void Job_class_should_calculate_attack_correctly()
    {
        GIVEN.a_warrior()
            .AND.its_attributes_are(new JobClassAttributes(100, 100, 100, 100))
            .AND.its_attack_is(job => job.Attributes.Strength * 0.10m + job.Attributes.Intelligence * 0.10m)
            .WHEN.calculate_its_attack()
            .THEN.resul_should_be(20)
            .AND.the_attack_description_should_not_be_empty();
    }
    
    [Fact]
    public void Job_class_should_calculate_speed_correctly()
    {
        GIVEN.a_warrior()
            .AND.its_attributes_are(new JobClassAttributes(100, 100, 100, 100))
            .AND.its_speed_is(job => job.Attributes.Strength * 0.05m + job.Attributes.Intelligence * 0.05m)
            .WHEN.calculate_its_speed()
            .THEN.resul_should_be(10)
            .AND.the_speed_description_should_not_be_empty();
    }    
}