using HospitalManagement.Services.Modules.SymptomsModule.Commands;
using HospitalManagement.Services.Modules.SymptomsModule.Models;
using HospitalManagement.Services.Modules.SymptomsModule.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class SymptomController(
    IMediator mediator) : ControllerBase
{
    [HttpGet("Query")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Symptom))]
    public Task<List<Symptom>> Query([FromQuery] QuerySymptom query)
    {
        return mediator.Send(query);
    }

    [HttpGet("Find")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Symptom))]
    public async Task<IActionResult> Find([FromQuery] FindSymptom query)
    {
        var record = await mediator.Send(query);

        return record switch
        {
            null => NotFound(),
            _ => Ok(record)
        };
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Symptom))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindById([FromRoute] int id)
    {
        var record = await mediator.Send(new FindSymptom().ById(id));

        return record switch
        {
            null => NotFound(),
            _ => Ok(record)
        };
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Symptom))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateSymptom command)
    {
        var record = await mediator.Send(command);

        return CreatedAtAction(nameof(FindById), new { id = record.Id }, record);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSymptom command)
    {
        await mediator.Send(command.UseId(id));
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await mediator.Send(new DeleteSymptom().UseId(id));

        return NoContent();
    }
}