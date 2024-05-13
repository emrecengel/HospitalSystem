using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.UsersModule.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.UsersModule.Commands;

public class DeleteUser : IRequest<Unit>
{
    internal long Id { get; set; }

    public DeleteUser UseId(long value)
    {
        Id = value;
        return this;
    }
}

internal sealed class HandleDeleteUser(
    IRepository<User> repository) : IRequestHandler<DeleteUser, Unit>
{
    public async Task<Unit> Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        var query = repository.Query;
        var model = await query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        await repository.Delete(model);
        return Unit.Value;
    }
}