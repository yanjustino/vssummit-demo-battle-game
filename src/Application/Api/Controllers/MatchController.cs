using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Domain.UseCases;
using Domain.UseCases.Commands;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class MatchController : ControllerBase
{
    private readonly OnStartingNewMatch _onCharactersFight;

    public MatchController(OnStartingNewMatch onCharactersFight)
    {
        _onCharactersFight = onCharactersFight;
    }
    
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status202Accepted, "Subscribe in app")]
    public async Task<IActionResult> Subscribe(StartNewMatch command)
    {
        _ = _onCharactersFight.Execute(command);
        return Accepted();
    }    
}