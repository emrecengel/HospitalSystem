using HospitalManagement.Services.Modules.PatientsModule.Commands;
using HospitalManagement.Services.Modules.PatientsModule.Models;
using HospitalManagement.Services.Modules.PatientsModule.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class PatientController(
    IMediator mediator) : ControllerBase
{
    [HttpGet("Query")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Patient))]
    public Task<List<Patient>> Query([FromQuery] QueryPatient query)
    {
        return mediator.Send(query);
    }

    [HttpGet("Find")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Patient))]
    public async Task<IActionResult> Find([FromQuery] FindPatient query)
    {
        var record = await mediator.Send(query);

        return record switch
        {
            null => NotFound(),
            _ => Ok(record)
        };
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Patient))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindById([FromRoute] int id)
    {
        var record = await mediator.Send(new FindPatient().ById(id));

        return record switch
        {
            null => NotFound(),
            _ => Ok(record)
        };
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Patient))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePatient command)
    {
        var record = await mediator.Send(command);

        return CreatedAtAction(nameof(FindById), new { id = record.Id }, record);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePatient command)
    {
        await mediator.Send(command.UseId(id));
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await mediator.Send(new DeletePatient().UseId(id));

        return NoContent();
    }
}