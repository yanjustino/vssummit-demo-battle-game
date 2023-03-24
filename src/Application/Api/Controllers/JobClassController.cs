using Api.ViewModels;
using Domain.Entities.JobClasses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class JobClassController : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get default job classes")]
    [SwaggerResponse(StatusCodes.Status200OK, "job classes", typeof(IEnumerable<JobClass>))]
    public async Task<IActionResult> Get()
    {
        var result = JobClassDefaultValues.ToDictionary();
        return Ok(result);
    }    
}