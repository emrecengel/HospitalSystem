using AutoMapper;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.SymptomsModule.Models;
using MediatR;

namespace HospitalManagement.Services.Modules.SymptomsModule.Commands;

public class CreateSymptom : IRequest<Symptom>
{
    public string Name { get; set; }
}

internal sealed class HandleCreateSymptom(
    IRepository<Symptom> repository,
    IMapper mapper) : IRequestHandler<CreateSymptom, Symptom>
{
    public Task<Symptom> Handle(CreateSymptom request, CancellationToken cancellationToken)
    {
        return repository.Create(mapper.Map<Symptom>(request));
    }
}