using AutoMapper;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.UserRolesModule.Models;
using MediatR;

namespace HospitalManagement.Services.Modules.UserRolesModule.Commands;

public class CreateUserRole : IRequest<UserRole>
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
}

internal sealed class HandleCreateUserRole(
    IRepository<UserRole> repository,
    IMapper mapper) : IRequestHandler<CreateUserRole, UserRole>
{
    public Task<UserRole> Handle(CreateUserRole request, CancellationToken cancellationToken)
    {
        return repository.Create(mapper.Map<UserRole>(request));
    }
}