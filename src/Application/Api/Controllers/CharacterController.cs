using System.Net;
using Api.ViewModels;
using Domain.Entities.Characters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Domain.UseCases;
using Domain.UseCases.Adapters.Queries;
using Domain.UseCases.Adapters.Repositories;
using Domain.UseCases.Commands;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class CharacterController : ControllerBase
{
    private readonly OnCreateNewCharacter _onCreateNewCharacter;
    private readonly ICharacterQueryable _characterQuery;

    public CharacterController(OnCreateNewCharacter onCreateNewCharacter, ICharacterQueryable characterQuery)
    {
        _onCreateNewCharacter = onCreateNewCharacter;
        _characterQuery = characterQuery;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [SwaggerOperation(Summary = "Register a new Character")]
    [SwaggerResponse(StatusCodes.Status202Accepted)]
    public IActionResult Post(CreateNewCharacter command)
    {
        _ = _onCreateNewCharacter.Execute(command);
        return Accepted();
    } 
    
    [HttpGet("{name:required}")]
    [SwaggerOperation(Summary = "Get character by name")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "character not found")]
    [SwaggerResponse(StatusCodes.Status200OK, "character", typeof(Character))]
    public async Task<IActionResult> Get(string name)
    {
        var result = await _characterQuery.Get(name);
        return result is null ? NotFound() : Ok(result);
    }     
    
    [HttpGet]
    [SwaggerOperation(Summary = "Get all characters")]
    [SwaggerResponse(StatusCodes.Status200OK, "characters", typeof(IEnumerable<CharacterViewModel>))]
    public async Task<IActionResult> Get()
    {
        var result = await _characterQuery.GetAll();
        var viewModel = CharacterViewModel.GetCharacterViewModel(result);
        return Ok(viewModel);
    } 
    
    [HttpGet("paginate")]
    [SwaggerOperation(Summary = "Paginate characters paginate")]
    [SwaggerResponse(StatusCodes.Status200OK, "characters", typeof(IEnumerable<CharacterViewModel>))]
    public async Task<IActionResult> Get([FromQuery]int page, [FromQuery]int size)
    {
        var result = await _characterQuery.Get(page, size);
        var viewModel = CharacterViewModel.GetCharacterViewModel(result);
        return Ok(viewModel);
    }     
}