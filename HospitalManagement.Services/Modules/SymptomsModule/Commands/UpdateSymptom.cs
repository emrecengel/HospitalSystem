using AutoMapper;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.SymptomsModule.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.SymptomsModule.Commands;

public class UpdateSymptom : IRequest<Unit>
{
    internal int Id { get; set; }
    public string Name { get; set; }

    public UpdateSymptom UseId(int value)
    {
        Id = value;
        return this;
    }
}

internal sealed class HandleUpdateSymptom(
    IRepository<Symptom> repository,
    IMapper mapper) : IRequestHandler<UpdateSymptom, Unit>
{
    public async Task<Unit> Handle(UpdateSymptom request, CancellationToken cancellationToken)
    {
        var query = repository.Query;
        var model = await query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        var updatedModel = mapper.Map(request, model);
        await repository.Update(updatedModel);
        return Unit.Value;
    }
}