using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.PatientsModule.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.PatientsModule.Commands;

public class DeletePatient : IRequest<Unit>
{
    internal int Id { get; set; }

    public DeletePatient UseId(int value)
    {
        Id = value;
        return this;
    }
}

internal sealed class HandleDeletePatient(
    IRepository<Patient> repository) : IRequestHandler<DeletePatient, Unit>
{
    public async Task<Unit> Handle(DeletePatient request, CancellationToken cancellationToken)
    {
        var query = repository.Query;
        var model = await query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        await repository.Delete(model);
        return Unit.Value;
    }
}