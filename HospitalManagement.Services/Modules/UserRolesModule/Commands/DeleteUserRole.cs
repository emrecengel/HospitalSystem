using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.UserRolesModule.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.UserRolesModule.Commands;

public class DeleteUserRole : IRequest<Unit>
{
    public int UserId { get; set; }
    public int RoleId { get; set; }

    public DeleteUserRole UseId(int userId, int roleId)
    {
        UserId = userId;
        RoleId = roleId;

        return this;
    }
}

internal sealed class HandleDeleteUserRole(
    IRepository<UserRole> repository) : IRequestHandler<DeleteUserRole, Unit>
{
    public async Task<Unit> Handle(DeleteUserRole request, CancellationToken cancellationToken)
    {
        var query = repository.Query;
        var model = await query.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.RoleId == request.RoleId,
            cancellationToken);

        await repository.Delete(model);
        return Unit.Value;
    }
}