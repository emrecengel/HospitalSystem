using HospitalManagement.Services.Modules.DoctorsModule.Commands;
using HospitalManagement.Services.Modules.DoctorsModule.Models;
using HospitalManagement.Services.Modules.DoctorsModule.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class DoctorController(
    IMediator mediator) : ControllerBase
{
    [HttpGet("Query")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Doctor))]
    public Task<List<Doctor>> Query([FromQuery] QueryDoctor query)
    {
        return mediator.Send(query);
    }

    [HttpGet("Find")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Doctor))]
    public async Task<IActionResult> Find([FromQuery] FindDoctor query)
    {
        var record = await mediator.Send(query);

        return record switch
        {
            null => NotFound(),
            _ => Ok(record)
        };
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Doctor))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindById([FromRoute] int id)
    {
        var record = await mediator.Send(new FindDoctor().ById(id));

        return record switch
        {
            null => NotFound(),
            _ => Ok(record)
        };
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Doctor))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateDoctor command)
    {
        var record = await mediator.Send(command);

        return CreatedAtAction(nameof(FindById), new { id = record.Id }, record);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDoctor command)
    {
        await mediator.Send(command.UseId(id));
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await mediator.Send(new DeleteDoctor().UseId(id));

        return NoContent();
    }
}