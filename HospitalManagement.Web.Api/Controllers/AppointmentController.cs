using HospitalManagement.Services.Modules.AppointmentsModule.Commands;
using HospitalManagement.Services.Modules.AppointmentsModule.Models;
using HospitalManagement.Services.Modules.AppointmentsModule.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class AppointmentController(
    IMediator mediator) : ControllerBase
{
    [HttpGet("Query")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Appointment))]
    public Task<List<Appointment>> Query([FromQuery] QueryAppointment query)
    {
        return mediator.Send(query);
    }

    [HttpGet("Find")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Appointment))]
    public async Task<IActionResult> Find([FromQuery] FindAppointment query)
    {
        var record = await mediator.Send(query);

        return record switch
        {
            null => NotFound(),
            _ => Ok(record)
        };
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Appointment))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindById([FromRoute] int id)
    {
        var record = await mediator.Send(new FindAppointment().ById(id));

        return record switch
        {
            null => NotFound(),
            _ => Ok(record)
        };
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Appointment))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAppointment command)
    {
        var record = await mediator.Send(command);

        return CreatedAtAction(nameof(FindById), new { id = record.Id }, record);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAppointment command)
    {
        await mediator.Send(command.UseId(id));
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await mediator.Send(new DeleteAppointment().UseId(id));

        return NoContent();
    }
}