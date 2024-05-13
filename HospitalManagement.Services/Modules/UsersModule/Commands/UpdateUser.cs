using AutoMapper;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.UsersModule.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.UsersModule.Commands;

public class UpdateUser : IRequest<Unit>
{
    internal int Id { get; set; }
    public string EmailAddress { get; set; }
    public string Password { get; set; }

    public UpdateUser UseId(int value)
    {
        Id = value;
        return this;
    }
}

internal sealed class HandleUpdateUser(
    IRepository<User> repository,
    IMapper mapper) : IRequestHandler<UpdateUser, Unit>
{
    public async Task<Unit> Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        var query = repository.Query;
        var model = await query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        var updatedModel = mapper.Map(request, model);
        await repository.Update(updatedModel);
        return Unit.Value;
    }
}