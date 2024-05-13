using AutoMapper;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.RolesModule.Models;
using MediatR;

namespace HospitalManagement.Services.Modules.RolesModule.Commands;

public class CreateRole : IRequest<Role>
{
    public string Name { get; set; }
    public string Description { get; set; }
}

internal sealed class HandleCreateRole(
    IRepository<Role> repository,
    IMapper mapper) : IRequestHandler<CreateRole, Role>
{
    public Task<Role> Handle(CreateRole request, CancellationToken cancellationToken)
    {
        return repository.Create(mapper.Map<Role>(request));
    }
}