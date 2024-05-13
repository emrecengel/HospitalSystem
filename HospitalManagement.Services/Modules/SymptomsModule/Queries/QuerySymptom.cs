using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.SymptomsModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.SymptomsModule.Queries;

public sealed class QuerySymptom : IRequest<List<Symptom>>, IFilterableRequest, IPageableRequest
{
    internal Func<IQueryable<Symptom>, IQueryable<Symptom>>? Query { get; set; }

    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public SortBy? SortBy { get; set; }
    public int? PageSize { get; set; }
    public int PageIndex { get; set; }

    public QuerySymptom ByQuery(Func<IQueryable<Symptom>, IQueryable<Symptom>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleQuerySymptom(
    IRepository<Symptom> repository) : IRequestHandler<QuerySymptom, List<Symptom>>
{
    public Task<List<Symptom>> Handle(QuerySymptom request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);


        query = query.ApplyStringFilters(request);

        return query.ToListAsync(cancellationToken);
    }
}