using Api.Controllers;
using C4Sharp.Diagrams;
using C4Sharp.Elements;
using C4Sharp.Elements.Boundaries;
using C4Sharp.Elements.Containers;
using C4Sharp.Elements.Relationships;
using Domain.UseCases;
using Domain.UseCases.Adapters.MessageBroker;
using Domain.UseCases.Adapters.Queries;
using Domain.UseCases.Adapters.Repositories;
using Infrastructure.MessageBroker.EventDriven;

namespace Architecture.Diagrams;

public class BattleGameOverview : BaseDiagram
{
    protected override string Title => "Battle Game Overview";
    protected override DiagramType DiagramType => DiagramType.Component;

    protected override IEnumerable<Structure> Structures => new Structure[]
    {
        new Person("player", "Player"),
        new ClientSideWebApp("spa", "Client HTTP", "Browser"),
        
        new ContainerBoundary("ApplicationBoundary", "Application")
        {
            Description = "Application Layer",
            Components = new Component[]
            {
                new Component<ChannelController>("dotnet", "API para pub/sub"),
                new Component<CharacterController>("dotnet", "API de personagens"),
                new Component<JobClassController>("dotnet", "API de profissões"),
                new Component<MatchController>("dotnet", "API de partidas"),
            }            
        },
        
        new ContainerBoundary("DomainBoundary", "Domain")
        {
            Description = "Domain Layer",
            Components = new Component[]
            {
                new Component<OnCreateNewCharacter>("Use case"),
                new Component<OnStartingNewMatch>("Use case"),
                new Component<IPublisher>("Kafka", "producer adapter"),
                new Component<ISubscriber>("kafka", "consumer adapter")
            }
        }, 
        
        new C4Sharp.Elements.Containers.Queue<MessagingQueue>("In memory"),
        new Database<ICharacterRepository>("In memory"),
        new Database<ICharacterQueryable>("In memory")        
    };

    protected override IEnumerable<Relationship> Relationships => new[]
    {
        It("player") > It("spa") | "use",
        It("spa") > It<CharacterController>() | "gerenciar personagens",
        It("spa") > It<ChannelController>() | "subscribe" | Position.Left,
        It("spa") > It<MatchController>() | "iniciar uma partida",
        It("spa") > It<JobClassController>() | "recuperar profissões",

        It<ChannelController>() > It<ISubscriber>() | "subscribe",
        It<CharacterController>() > It<OnCreateNewCharacter>() | "command",
        It<MatchController>() > It<OnStartingNewMatch>() | "command",

        It<OnCreateNewCharacter>() > It<IPublisher>() | "publish" | Position.Down,
        It<OnCreateNewCharacter>() > It<ICharacterRepository>() | "write" | Position.Right,
        It<OnStartingNewMatch>() > It<IPublisher>() | "publish" | Position.Left,

        It<CharacterController>() > It<ICharacterQueryable>() | "read" | Position.Up,
        It<ISubscriber>() < It<MessagingQueue>() | "notify",
        It<IPublisher>() > It<MessagingQueue>() | "publish" | Position.Left,
    };
}