using HospitalManagement.Services.Modules.UserRolesModule.Queries;
using HospitalManagement.Services.Modules.UsersModule.Commands;
using HospitalManagement.Services.Modules.UsersModule.Models;
using HospitalManagement.Services.Modules.UsersModule.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class UserController(
    IMediator mediator) : ControllerBase
{
    [HttpGet("Query")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    public Task<List<User>> Query([FromQuery] QueryUser query)
    {
        return mediator.Send(query);
    }

    [HttpGet("Find")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    public async Task<IActionResult> Find([FromQuery] FindUser query)
    {
        var record = await mediator.Send(query);

        return record switch
        {
            null => NotFound(),
            _ => Ok(record)
        };
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindById([FromRoute] int id)
    {
        var record = await mediator.Send(new FindUser().ById(id));

        return record switch
        {
            null => NotFound(),
            _ => Ok(record)
        };
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(User))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUser command)
    {
        var record = await mediator.Send(command);

        return CreatedAtAction(nameof(FindById), new { id = record.Id }, record);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUser command)
    {
        await mediator.Send(command.UseId(id));
        return NoContent();
    }


    [HttpGet("{id:int}/role")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        
        var query = new QueryUserRole().ByQuery(q => q.Where(x => x.UserId == id));

        return Ok(await mediator.Send(query));
    }

    [HttpPost("{id:int}/Role/{roleId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignRole([FromRoute] int id, [FromRoute] int roleId)
    {
        var command = new AssignRoleToUser()
            .UseUserId(id)
            .UseRoleId(roleId);

        await mediator.Send(command);

        return NoContent();
    }


    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await mediator.Send(new DeleteUser().UseId(id));

        return NoContent();
    }
}