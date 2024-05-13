using HospitalManagement.Services.Modules.SymptomDiagnosesModule.Commands;
using MediatR;

namespace HospitalManagement.Services.Modules.DiagnosesModule.Commands;

public class AssignSymptomToDiagnosis : IRequest<Unit>
{
    internal int SymptomId { get; set; }
    internal int DiagnosisId { get; set; }

    public AssignSymptomToDiagnosis UseSymptomId(int value)
    {
        SymptomId = value;
        return this;
    }

    public AssignSymptomToDiagnosis UseDiagnosisId(int value)
    {
        DiagnosisId = value;
        return this;
    }
}

internal sealed class HandleAssignSymptomToDiagnosis(
    IMediator mediator) : IRequestHandler<AssignSymptomToDiagnosis, Unit>
{
    public async Task<Unit> Handle(AssignSymptomToDiagnosis request, CancellationToken cancellationToken)
    {
        var command = new CreateSymptomDiagnosis
        {
            SymptomId = request.SymptomId,
            DiagnosisId = request.DiagnosisId
        };

        await mediator.Send(command, cancellationToken);

        return Unit.Value;
    }
}