using AutoMapper;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.RolesModule.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.RolesModule.Commands;

public class UpdateRole : IRequest<Unit>
{
    internal int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public UpdateRole UseId(int value)
    {
        Id = value;
        return this;
    }
}

internal sealed class HandleUpdateRole(
    IRepository<Role> repository,
    IMapper mapper) : IRequestHandler<UpdateRole, Unit>
{
    public async Task<Unit> Handle(UpdateRole request, CancellationToken cancellationToken)
    {
        var query = repository.Query;
        var model = await query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        var updatedModel = mapper.Map(request, model);
        await repository.Update(updatedModel);
        return Unit.Value;
    }
}