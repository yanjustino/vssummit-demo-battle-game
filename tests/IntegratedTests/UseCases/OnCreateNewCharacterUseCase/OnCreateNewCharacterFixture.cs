using Domain.Entities.Characters;
using Moq;
using Domain.UseCases.Commands;
using Domain.UseCases.Events;

namespace IntegratedTests.UseCases.OnCreateNewCharacterUseCase;

internal static class OnCreateNewCharacterFixture
{
    public static OnCreateNewCharacterContext a_command(this OnCreateNewCharacterContext cotext) =>
        cotext with
        {
            Command = new CreateNewCharacter
            {
                SubscriptionKey = Guid.NewGuid().ToString(), Name = "character_name", JobClassName = "job_class_name"    
            }
        };

    public static OnCreateNewCharacterContext its_job_name_is(this OnCreateNewCharacterContext context, string value)
    {
        context.Command = context.Command with { JobClassName = value };
        return context;
    }
    
    public static OnCreateNewCharacterContext its_character_name_is(this OnCreateNewCharacterContext context, string value)
    {
        context.Command = context.Command with { Name = value };
        return context;
    }    
    
    public static OnCreateNewCharacterContext this_name_already_registered(this OnCreateNewCharacterContext context)
    {
        context.CharacterRepository.Setup(s => s.Save(It.IsAny<Character>()))
            .ReturnsAsync(() => (true, "Character Already Registered"));
        return context;
    }    
    
    public static OnCreateNewCharacterContext usecase_is_executed(this OnCreateNewCharacterContext context)
    {
        context.UseCase.Execute(context.Command);
        return context;
    }

    public static OnCreateNewCharacterContext JobClassNotFound_event_should_be_notified(this OnCreateNewCharacterContext context)
    {
        context.Publisher.Verify(s => s.Notify(It.IsAny<JobClassNotFound>()), Times.Once);
        return context;
    }
    
    public static OnCreateNewCharacterContext the_failure_event_should_be_notified(this OnCreateNewCharacterContext context)
    {
        context.Publisher.Verify(s => s.Notify(It.IsAny<CharacterNotRegistered>()), Times.Once);
        return context;
    }    
    
    public static OnCreateNewCharacterContext success_event_should_be_notified(this OnCreateNewCharacterContext context)
    {
        context.Publisher.Verify(s => s.Notify(It.IsAny<CharacterRegistered>()));
        return context;
    }      
    
    public static OnCreateNewCharacterContext save_method_should_be_executed(this OnCreateNewCharacterContext context, int times = 1)
    {
        context.CharacterRepository.Verify(s => s.Save(It.IsAny<Character>()), Times.Exactly(times));
        return context;
    }   
    
    public static OnCreateNewCharacterContext save_method_should_not_be_executed(this OnCreateNewCharacterContext context)
    {
        context.CharacterRepository.Verify(s => s.Save(It.IsAny<Character>()), Times.Never);
        return context;
    }     
}