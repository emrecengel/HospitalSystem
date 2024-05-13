using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.PatientsModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.PatientsModule.Queries;

public sealed class QueryPatient : IRequest<List<Patient>>, IFilterableRequest, IPageableRequest
{
    internal Func<IQueryable<Patient>, IQueryable<Patient>>? Query { get; set; }

    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public SortBy? SortBy { get; set; }
    public int? PageSize { get; set; }
    public int PageIndex { get; set; }

    public QueryPatient ByQuery(Func<IQueryable<Patient>, IQueryable<Patient>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleQueryPatient(
    IRepository<Patient> repository) : IRequestHandler<QueryPatient, List<Patient>>
{
    public Task<List<Patient>> Handle(QueryPatient request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);


        query = query.ApplyStringFilters(request);

        return query.ToListAsync(cancellationToken);
    }
}