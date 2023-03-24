using static IntegratedTests.UseCases.OnCreateNewCharacterUseCase.OnCreateNewCharacterContext;

namespace IntegratedTests.UseCases.OnCreateNewCharacterUseCase;

public class OnCreateNewCharacterTests
{
    [Fact]
    public void Try_creating_a_character_with_unknown_job_name()
    {
        GIVEN.a_command()
            .AND.its_job_name_is("Unknown")
            .WHEN.usecase_is_executed()
            .THEN.JobClassNotFound_event_should_be_notified()
            .AND.save_method_should_not_be_executed();
    }
    
    [Fact]
    public void Try_creating_a_character_with_invalid_name()
    {
        GIVEN.a_command()
            .AND.its_job_name_is("Warrior")
            .AND.its_character_name_is("123_character_invalid_name")
            .WHEN.usecase_is_executed()
            .THEN.the_failure_event_should_be_notified()
            .AND. save_method_should_not_be_executed();
    }    
    
    [Fact]
    public void Try_creating_a_character_already_registered()
    {
        GIVEN.a_command()
            .AND.its_job_name_is("Warrior")
            .AND.its_character_name_is("valid_name")
            .AND.this_name_already_registered()
            .WHEN.usecase_is_executed()
            .THEN.the_failure_event_should_be_notified()
            .AND. save_method_should_be_executed();
    }     
    
    [Fact]
    public void Try_creating_a_new_character()
    {
        GIVEN.a_command()
            .AND.its_job_name_is("Warrior")
            .AND.its_character_name_is("valid_name")
            .WHEN.usecase_is_executed()
            .THEN.success_event_should_be_notified()
            .AND.save_method_should_be_executed();
    }     
}