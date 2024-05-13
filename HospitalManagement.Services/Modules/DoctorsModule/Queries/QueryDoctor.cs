using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.DoctorsModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.DoctorsModule.Queries;

public sealed class QueryDoctor : IRequest<List<Doctor>>, IFilterableRequest, IPageableRequest
{
    internal Func<IQueryable<Doctor>, IQueryable<Doctor>>? Query { get; set; }

    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public SortBy? SortBy { get; set; }
    public int? PageSize { get; set; }
    public int PageIndex { get; set; }

    public QueryDoctor ByQuery(Func<IQueryable<Doctor>, IQueryable<Doctor>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleQueryDoctor(
    IRepository<Doctor> repository) : IRequestHandler<QueryDoctor, List<Doctor>>
{
    public Task<List<Doctor>> Handle(QueryDoctor request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);


        query = query.ApplyStringFilters(request);

        return query.ToListAsync(cancellationToken);
    }
}