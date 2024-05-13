using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.RolesModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.RolesModule.Queries;

public sealed class QueryRole : IRequest<List<Role>>, IFilterableRequest, IPageableRequest
{
    internal Func<IQueryable<Role>, IQueryable<Role>>? Query { get; set; }

    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public SortBy? SortBy { get; set; }
    public int? PageSize { get; set; }
    public int PageIndex { get; set; }

    public QueryRole ByQuery(Func<IQueryable<Role>, IQueryable<Role>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleQueryRole(
    IRepository<Role> repository) : IRequestHandler<QueryRole, List<Role>>
{
    public Task<List<Role>> Handle(QueryRole request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);


        query = query.ApplyStringFilters(request);

        return query.ToListAsync(cancellationToken);
    }
}