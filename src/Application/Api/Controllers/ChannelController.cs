using Infrastructure.MessageBroker.EventDriven;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Domain.UseCases.Adapters.MessageBroker;
using Domain.UseCases.Adapters.Repositories;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ChannelController : ControllerBase
{
    private readonly ILogger<CharacterController> _logger;
    private readonly ISubscriber _subscriber;
    private readonly MessagingQueue _queue;

    public ChannelController(ILogger<CharacterController> logger, MessagingQueue queue, ISubscriber subscriber)
    {
        _logger = logger;
        _subscriber = subscriber;
        _queue = queue;
    }

    [HttpPost("subscribe")]
    [SwaggerResponse(StatusCodes.Status200OK, "Subscribe in app")]
    public async Task<IActionResult> Subscribe()
    {
        //This is like a Promises
        async void Action(string subscription, object message)
        {
            await _queue.Push(subscription, message);
            _logger.LogInformation("Event {subscription}: {message} dispatched", subscription, message);
        }

        var subscription = await _subscriber.Subscribe(Action);
        return Ok(new { Subscription = subscription });
    }

    [HttpGet("{subscription:required}/events")]
    [SwaggerOperation(Summary = "Get subscription event")]
    [SwaggerResponse(StatusCodes.Status200OK, "Get message", typeof(Envelope<IEvent>))]
    public async Task<IActionResult> Get(string subscription)
    {
        var message = await _queue.Pop(subscription);
        return Ok(message);
    }
}