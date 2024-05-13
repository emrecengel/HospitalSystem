using AutoMapper;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.DiagnosesModule.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.DiagnosesModule.Commands;

public class UpdateDiagnosis : IRequest<Unit>
{
    internal long Id { get; set; }
    public string Name { get; set; }

    public UpdateDiagnosis UseId(long value)
    {
        Id = value;
        return this;
    }
}

internal sealed class HandleUpdateDiagnosis(
    IRepository<Diagnosis> repository,
    IMapper mapper) : IRequestHandler<UpdateDiagnosis, Unit>
{
    public async Task<Unit> Handle(UpdateDiagnosis request, CancellationToken cancellationToken)
    {
        var query = repository.Query;
        var model = await query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        mapper.Map(request, model);
        await repository.Update(model);
        return Unit.Value;
    }
}