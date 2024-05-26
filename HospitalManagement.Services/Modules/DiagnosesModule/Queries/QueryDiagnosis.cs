using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.DiagnosesModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.DiagnosesModule.Queries;

public sealed class QueryDiagnosis : IRequest<List<Diagnosis>>, IFilterableRequest, IPageableRequest
{
    internal Func<IQueryable<Diagnosis>, IQueryable<Diagnosis>>? Query { get; set; }
    public int[]? SymptomIds { get; set; }

    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public SortBy? SortBy { get; set; }
    public int? PageSize { get; set; }
    public int PageIndex { get; set; }

    public QueryDiagnosis ByQuery(Func<IQueryable<Diagnosis>, IQueryable<Diagnosis>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleQueryDiagnosis(
    IRepository<Diagnosis> repository) : IRequestHandler<QueryDiagnosis, List<Diagnosis>>
{
    public Task<List<Diagnosis>> Handle(QueryDiagnosis request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);


        if (request.SymptomIds != null)
            query = query.Where(x => x.Symptoms.Any(y => request.SymptomIds.Contains(y.SymptomId))).OrderByDescending(x=> x.Symptoms.Count());

        query = query.ApplyStringFilters(request);

        return query.ToListAsync(cancellationToken);
    }
}