using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.SymptomDiagnosesModule.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.SymptomDiagnosesModule.Commands;

public class DeleteSymptomDiagnosis : IRequest<Unit>
{
    internal int SymptomId { get; set; }
    internal int DiagnosisId { get; set; }

    public DeleteSymptomDiagnosis UseId(int symptomId, int diagnosisId)
    {
        SymptomId = symptomId;
        DiagnosisId = diagnosisId;
        return this;
    }
}

internal sealed class HandleDeleteSymptomDiagnosis(
    IRepository<SymptomDiagnosis> repository) : IRequestHandler<DeleteSymptomDiagnosis, Unit>
{
    public async Task<Unit> Handle(DeleteSymptomDiagnosis request, CancellationToken cancellationToken)
    {
        var query = repository.Query;
        var model = await query.FirstOrDefaultAsync(
            x => x.SymptomId == request.SymptomId && x.DiagnosisId == request.DiagnosisId, cancellationToken);

        await repository.Delete(model);
        return Unit.Value;
    }
}