using HospitalManagement.Services.Modules.UserRolesModule.Commands;
using MediatR;

namespace HospitalManagement.Services.Modules.UsersModule.Commands;

public class AssignRoleToUser : IRequest<Unit>
{
    internal int UserId { get; set; }
    internal int RoleId { get; set; }

    public AssignRoleToUser UseUserId(int value)
    {
        UserId = value;
        return this;
    }

    public AssignRoleToUser UseRoleId(int value)
    {
        RoleId = value;
        return this;
    }
}

internal sealed class HandleAssignRoleToUser(
    IMediator mediator) : IRequestHandler<AssignRoleToUser, Unit>
{
    public async Task<Unit> Handle(AssignRoleToUser request, CancellationToken cancellationToken)
    {
        var command = new CreateUserRole
        {
            UserId = request.UserId,
            RoleId = request.RoleId
        };

        await mediator.Send(command, cancellationToken);

        return Unit.Value;
    }
}