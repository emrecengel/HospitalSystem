using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.DiagnosesModule.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.DiagnosesModule.Commands;

public class DeleteDiagnosis : IRequest<Unit>
{
    internal long Id { get; set; }

    public DeleteDiagnosis UseId(long value)
    {
        Id = value;
        return this;
    }
}

internal sealed class HandleDeleteDiagnosis(
    IRepository<Diagnosis> repository) : IRequestHandler<DeleteDiagnosis, Unit>
{
    public async Task<Unit> Handle(DeleteDiagnosis request, CancellationToken cancellationToken)
    {
        var query = repository.Query;
        var model = await query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        await repository.Delete(model);
        return Unit.Value;
    }
}