using Domain.Entities;
using Domain.Entities.Characters;
using Domain.Entities.JobClasses;
using Domain.UseCases.Adapters.MessageBroker;
using Domain.UseCases.Adapters.Repositories;
using Domain.UseCases.Commands;

namespace Domain.UseCases;

public class OnCreateNewCharacter
{
    private IPublisher Publisher { get; }
    private IJobClassRepository JobClassRepository { get; }
    private ICharacterRepository CharacterRepository { get; }

    public OnCreateNewCharacter(IJobClassRepository jobClassRepository, ICharacterRepository characterRepository,
        IPublisher publisher)
    {
        Publisher = publisher;
        JobClassRepository = jobClassRepository;
        CharacterRepository = characterRepository;
    }

    public async Task Execute(CreateNewCharacter command)
    {
        var job = await GetJobClass(command);
        await RegisterCharacter(command, job);
    }

    private async Task<JobClass?> GetJobClass(CreateNewCharacter command)
    {
        var job = JobClassRepository.GetByName(command.JobClassName);
        if (job is null) await Publisher.Notify(command.ToJobClassNotFound());
        return job;
    }

    private async Task RegisterCharacter(CreateNewCharacter command, JobClass? job)
    {
        try
        {
            if (job is null) return;

            var character = new Character(command.Name, job);
            var (fail, error) = await CharacterRepository.Save(character);

            if (fail) await Publisher.Notify(command.ToCharacterNotRegistered(error));
            else await Publisher.Notify(command.ToCharacterRegistered(character));
        }
        catch (EntityArgumentException e)
        {
            await Publisher.Notify(command.ToCharacterNotRegistered(e.Message));
        }
    }
}