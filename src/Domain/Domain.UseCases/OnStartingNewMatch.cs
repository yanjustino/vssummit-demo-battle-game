using Domain.Entities.Characters;
using Domain.Entities.Matches;
using Domain.UseCases.Adapters.MessageBroker;
using Domain.UseCases.Adapters.Queries;
using Domain.UseCases.Adapters.Repositories;
using Domain.UseCases.Commands;
using Domain.UseCases.Events;

namespace Domain.UseCases;

public class OnStartingNewMatch
{
    private IPublisher Publisher { get; }
    private ICharacterQueryable Query { get; }    
    private ICharacterRepository Repository { get; }    
    
    public OnStartingNewMatch(IPublisher publisher, ICharacterQueryable query, ICharacterRepository repository)
    {
        Publisher = publisher;
        Query = query;
        Repository = repository;
    }

    public async Task Execute(StartNewMatch command)
    {
        var playerA = await Query.Get(command.FirstPlayer);
        var playerB = await Query.Get(command.SecondPlayer);

        if (playerA is null)
        {
            await Publisher.Notify(command.ToCharacterNotFound(command.FirstPlayer));
            return;
        }
        
        if (playerA.IsDead())
        {
            await Publisher.Notify(command.ToCharacterNotAlive(command.FirstPlayer));
            return;
        }        
        
        if (playerB is null)
        {
            await Publisher.Notify(command.ToCharacterNotFound(command.SecondPlayer));
            return;
        }
        
        if (playerB.IsDead())
        {
            await Publisher.Notify(command.ToCharacterNotAlive(command.SecondPlayer));
            return;
        }         

        var match = new Match(playerA, playerB);
        await match.Start();

        await Repository.Save(match.PlayerA);
        await Repository.Save(match.PlayerB);

        await Publisher.Notify(command.ToMatchFinished(match));
    }
}