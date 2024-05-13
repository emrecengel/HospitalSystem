using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.SymptomsModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.SymptomsModule.Queries;

public sealed class CountSymptom : IRequest<long>, ICountableRequest
{
    internal Func<IQueryable<Symptom>, IQueryable<Symptom>>? Query { get; set; }
    public string? Filter { get; set; }

    public CountSymptom ByQuery(Func<IQueryable<Symptom>, IQueryable<Symptom>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleCountSymptom(
    IRepository<Symptom> repository) : IRequestHandler<CountSymptom, long>
{
    public Task<long> Handle(CountSymptom request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);

        query = query.ApplyStringFilters(request);

        return query.LongCountAsync(cancellationToken);
    }
}