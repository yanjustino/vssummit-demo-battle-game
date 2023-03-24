using Infrastructure.Data.Repositories;
using Infrastructure.Data.Storage;
using Moq;
using Domain.UseCases;
using Domain.UseCases.Adapters.MessageBroker;
using Domain.UseCases.Adapters.Repositories;
using Domain.UseCases.Commands;

namespace IntegratedTests.UseCases.OnCreateNewCharacterUseCase;

internal record OnCreateNewCharacterContext
{
    public Mock<IPublisher> Publisher { get; set; } = new ();
    public Mock<IJobClassRepository> JobRepository { get; set; } = new ();
    public Mock<ICharacterRepository> CharacterRepository { get; set; } = new ();
    public OnCreateNewCharacter UseCase { get; set; }
    public CreateNewCharacter Command { get; set; } = new();

    public static OnCreateNewCharacterContext GIVEN => new();
    public OnCreateNewCharacterContext WHEN => this;
    public OnCreateNewCharacterContext THEN => this;
    public OnCreateNewCharacterContext AND => this;

    public OnCreateNewCharacterContext()
    {
        var jobRepository = new JobClassRepository(new DataContext());
        
        UseCase = new OnCreateNewCharacter(jobRepository, CharacterRepository.Object, Publisher.Object);
    }
}