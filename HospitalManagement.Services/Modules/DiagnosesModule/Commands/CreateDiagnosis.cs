using AutoMapper;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.DiagnosesModule.Models;
using MediatR;

namespace HospitalManagement.Services.Modules.DiagnosesModule.Commands;

public class CreateDiagnosis : IRequest<Diagnosis>
{
    public string Name { get; set; }
}

internal sealed class HandleCreateDiagnosis(
    IRepository<Diagnosis> repository,
    IMapper mapper) : IRequestHandler<CreateDiagnosis, Diagnosis>
{
    public Task<Diagnosis> Handle(CreateDiagnosis request, CancellationToken cancellationToken)
    {
        return repository.Create(mapper.Map<Diagnosis>(request));
    }
}