using AutoMapper;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.PatientsModule.Models;
using MediatR;

namespace HospitalManagement.Services.Modules.PatientsModule.Commands;

public class CreatePatient : IRequest<Patient>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
    public string Address { get; set; }
    public string BloodType { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
}

internal sealed class HandleCreatePatient(
    IRepository<Patient> repository,
    IMapper mapper) : IRequestHandler<CreatePatient, Patient>
{
    public Task<Patient> Handle(CreatePatient request, CancellationToken cancellationToken)
    {
        return repository.Create(mapper.Map<Patient>(request));
    }
}