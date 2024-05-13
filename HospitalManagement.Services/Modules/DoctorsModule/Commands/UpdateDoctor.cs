using AutoMapper;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.DoctorsModule.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.DoctorsModule.Commands;

public class UpdateDoctor : IRequest<Unit>
{
    internal int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Specialization { get; set; }
    public int? UserId { get; set; }
    public string Availability { get; set; }

    public UpdateDoctor UseId(int value)
    {
        Id = value;
        return this;
    }
}

internal sealed class HandleUpdateDoctor(
    IRepository<Doctor> repository,
    IMapper mapper) : IRequestHandler<UpdateDoctor, Unit>
{
    public async Task<Unit> Handle(UpdateDoctor request, CancellationToken cancellationToken)
    {
        var query = repository.Query;
        var model = await query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        mapper.Map(request, model);
        await repository.Update(model);
        return Unit.Value;
    }
}