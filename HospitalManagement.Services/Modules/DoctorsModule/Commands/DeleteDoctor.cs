using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.DoctorsModule.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.DoctorsModule.Commands;

public class DeleteDoctor : IRequest<Unit>
{
    internal long Id { get; set; }

    public DeleteDoctor UseId(long value)
    {
        Id = value;
        return this;
    }
}

internal sealed class HandleDeleteDoctor(
    IRepository<Doctor> repository) : IRequestHandler<DeleteDoctor, Unit>
{
    public async Task<Unit> Handle(DeleteDoctor request, CancellationToken cancellationToken)
    {
        var query = repository.Query;
        var model = await query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        await repository.Delete(model);
        return Unit.Value;
    }
}