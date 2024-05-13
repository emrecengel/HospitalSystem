using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.SymptomDiagnosesModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.SymptomDiagnosesModule.Queries;

public sealed class QuerySymptomDiagnosis : IRequest<List<SymptomDiagnosis>>, IFilterableRequest, IPageableRequest
{
    internal Func<IQueryable<SymptomDiagnosis>, IQueryable<SymptomDiagnosis>>? Query { get; set; }

    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public SortBy? SortBy { get; set; }
    public int? PageSize { get; set; }
    public int PageIndex { get; set; }

    public QuerySymptomDiagnosis ByQuery(Func<IQueryable<SymptomDiagnosis>, IQueryable<SymptomDiagnosis>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleQuerySymptomDiagnosis(
    IRepository<SymptomDiagnosis> repository) : IRequestHandler<QuerySymptomDiagnosis, List<SymptomDiagnosis>>
{
    public Task<List<SymptomDiagnosis>> Handle(QuerySymptomDiagnosis request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);


        query = query.ApplyStringFilters(request);

        return query.ToListAsync(cancellationToken);
    }
}