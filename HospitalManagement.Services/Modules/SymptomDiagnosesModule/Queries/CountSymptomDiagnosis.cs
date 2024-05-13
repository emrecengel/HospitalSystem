using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.SymptomDiagnosesModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.SymptomDiagnosesModule.Queries;

public sealed class CountSymptomDiagnosis : IRequest<long>, ICountableRequest
{
    internal Func<IQueryable<SymptomDiagnosis>, IQueryable<SymptomDiagnosis>>? Query { get; set; }
    public string? Filter { get; set; }

    public CountSymptomDiagnosis ByQuery(Func<IQueryable<SymptomDiagnosis>, IQueryable<SymptomDiagnosis>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleCountSymptomDiagnosis(
    IRepository<SymptomDiagnosis> repository) : IRequestHandler<CountSymptomDiagnosis, long>
{
    public Task<long> Handle(CountSymptomDiagnosis request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);

        query = query.ApplyStringFilters(request);

        return query.LongCountAsync(cancellationToken);
    }
}