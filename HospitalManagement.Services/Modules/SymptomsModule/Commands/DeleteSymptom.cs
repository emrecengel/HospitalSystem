using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.SymptomsModule.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.SymptomsModule.Commands;

public class DeleteSymptom : IRequest<Unit>
{
    internal long Id { get; set; }

    public DeleteSymptom UseId(long value)
    {
        Id = value;
        return this;
    }
}

internal sealed class HandleDeleteSymptom(
    IRepository<Symptom> repository) : IRequestHandler<DeleteSymptom, Unit>
{
    public async Task<Unit> Handle(DeleteSymptom request, CancellationToken cancellationToken)
    {
        var query = repository.Query;
        var model = await query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        await repository.Delete(model);
        return Unit.Value;
    }
}