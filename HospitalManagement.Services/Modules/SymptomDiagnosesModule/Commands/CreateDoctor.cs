using AutoMapper;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.SymptomDiagnosesModule.Models;
using MediatR;

namespace HospitalManagement.Services.Modules.SymptomDiagnosesModule.Commands;

public class CreateSymptomDiagnosis : IRequest<SymptomDiagnosis>
{
    public int SymptomId { get; set; }
    public int DiagnosisId { get; set; }
}

internal sealed class HandleCreateSymptomDiagnosis(
    IRepository<SymptomDiagnosis> repository,
    IMapper mapper) : IRequestHandler<CreateSymptomDiagnosis, SymptomDiagnosis>
{
    public Task<SymptomDiagnosis> Handle(CreateSymptomDiagnosis request, CancellationToken cancellationToken)
    {
        return repository.Create(mapper.Map<SymptomDiagnosis>(request));
    }
}