using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.RolesModule.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.RolesModule.Commands;

public class DeleteRole : IRequest<Unit>
{
    internal long Id { get; set; }

    public DeleteRole UseId(long value)
    {
        Id = value;
        return this;
    }
}

internal sealed class HandleDeleteRole(
    IRepository<Role> repository) : IRequestHandler<DeleteRole, Unit>
{
    public async Task<Unit> Handle(DeleteRole request, CancellationToken cancellationToken)
    {
        var query = repository.Query;
        var model = await query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        await repository.Delete(model);
        return Unit.Value;
    }
}