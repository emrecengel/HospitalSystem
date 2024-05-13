using AutoMapper;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.DoctorsModule.Models;
using MediatR;

namespace HospitalManagement.Services.Modules.DoctorsModule.Commands;

public class CreateDoctor : IRequest<Doctor>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Specialization { get; set; }
    public string Availability { get; set; }
}

internal sealed class HandleCreateDoctor(
    IRepository<Doctor> repository,
    IMapper mapper) : IRequestHandler<CreateDoctor, Doctor>
{
    public Task<Doctor> Handle(CreateDoctor request, CancellationToken cancellationToken)
    {
        return repository.Create(mapper.Map<Doctor>(request));
    }
}