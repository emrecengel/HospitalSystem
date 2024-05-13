using HospitalManagement.Services.Modules.DiagnosesModule.Commands;
using HospitalManagement.Services.Modules.DiagnosesModule.Models;
using HospitalManagement.Services.Modules.DiagnosesModule.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class DiagnosisController(
    IMediator mediator) : ControllerBase
{
    [HttpGet("Query")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Diagnosis))]
    public Task<List<Diagnosis>> Query([FromQuery] QueryDiagnosis query)
    {
        return mediator.Send(query);
    }

    [HttpGet("Find")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Diagnosis))]
    public async Task<IActionResult> Find([FromQuery] FindDiagnosis query)
    {
        var record = await mediator.Send(query);

        return record switch
        {
            null => NotFound(),
            _ => Ok(record)
        };
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Diagnosis))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindById([FromRoute] int id)
    {
        var record = await mediator.Send(new FindDiagnosis().ById(id));

        return record switch
        {
            null => NotFound(),
            _ => Ok(record)
        };
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Diagnosis))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateDiagnosis command)
    {
        var record = await mediator.Send(command);

        return CreatedAtAction(nameof(FindById), new { id = record.Id }, record);
    }


    [HttpPost("{id:int}/symptom/{symptomId:int}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Diagnosis))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddSymptom([FromRoute] int id, [FromRoute] int symptomId)
    {
        await mediator.Send(new AssignSymptomToDiagnosis().UseDiagnosisId(id).UseSymptomId(symptomId));

        return NoContent();
    }


    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDiagnosis command)
    {
        await mediator.Send(command.UseId(id));
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await mediator.Send(new DeleteDiagnosis().UseId(id));

        return NoContent();
    }
}