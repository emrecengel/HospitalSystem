using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.DiagnosesModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.DiagnosesModule.Queries;

public sealed class CountDiagnosis : IRequest<long>, ICountableRequest
{
    internal Func<IQueryable<Diagnosis>, IQueryable<Diagnosis>>? Query { get; set; }
    public string? Filter { get; set; }

    public CountDiagnosis ByQuery(Func<IQueryable<Diagnosis>, IQueryable<Diagnosis>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleCountDiagnosis(
    IRepository<Diagnosis> repository) : IRequestHandler<CountDiagnosis, long>
{
    public Task<long> Handle(CountDiagnosis request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);

        query = query.ApplyStringFilters(request);

        return query.LongCountAsync(cancellationToken);
    }
}