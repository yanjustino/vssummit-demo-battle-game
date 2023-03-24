namespace FitnessFunction.Maintainability;

public class MaintainabilityFixture
{
    public string EventNamespace => "Domain.UseCases.Events";    

    public Architecture Architecture =>
        new ArchLoader().LoadAssemblies(
            typeof(Api.Controllers.ChannelController).Assembly, 
            typeof(Domain.UseCases.OnCreateNewCharacter).Assembly, 
            typeof(Domain.Entities.Characters.Character).Assembly, 
            typeof(Infrastructure.Data.Repositories.CharacterRepository).Assembly, 
            typeof(Infrastructure.MessageBroker.EventDriven.EventManager).Assembly).Build();

    public IObjectProvider<Class> EventClasses =>
        Classes().That().ImplementInterface("Domain.UseCases.Adapters.MessageBroker.IEvent")
            .As("Event Class");
    
    public IObjectProvider<Class> RepositoriesClasses =>
        Classes().That().ResideInNamespace("Infrastructure.Data.Repositories")
            .As("Repository");

    public IObjectProvider<Class> ApiControllers =>
        Classes().That().ResideInNamespace("Api.Controllers")
            .As("Controllers");     
    
    public IObjectProvider<Interface> AdapterRepository =>
        Interfaces().That().ResideInNamespace("Domain.UseCases.Adapters.Repositories")
            .As("Repository Adapters");

    public IObjectProvider<Interface> AdapterQueries =>
        Interfaces().That().ResideInNamespace("Domain.UseCases.Adapters.Queries").As("Query Adapters");
}