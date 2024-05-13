using AutoMapper;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.PatientsModule.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.PatientsModule.Commands;

public class UpdatePatient : IRequest<Unit>
{
    internal int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
    public string Address { get; set; }
    public string BloodType { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }

    public UpdatePatient UseId(int value)
    {
        Id = value;
        return this;
    }
}

internal sealed class HandleUpdatePatient(
    IRepository<Patient> repository,
    IMapper mapper) : IRequestHandler<UpdatePatient, Unit>
{
    public async Task<Unit> Handle(UpdatePatient request, CancellationToken cancellationToken)
    {
        var query = repository.Query;
        var model = await query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        var updatedModel = mapper.Map(request, model);
        await repository.Update(updatedModel);
        return Unit.Value;
    }
}