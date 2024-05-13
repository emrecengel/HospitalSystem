using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.UserRolesModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.UserRolesModule.Queries;

public sealed class QueryUserRole : IRequest<List<UserRole>>, IFilterableRequest, IPageableRequest
{
    internal Func<IQueryable<UserRole>, IQueryable<UserRole>>? Query { get; set; }

    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public SortBy? SortBy { get; set; }
    public int? PageSize { get; set; }
    public int PageIndex { get; set; }

    public QueryUserRole ByQuery(Func<IQueryable<UserRole>, IQueryable<UserRole>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleQueryUserRole(
    IRepository<UserRole> repository) : IRequestHandler<QueryUserRole, List<UserRole>>
{
    public Task<List<UserRole>> Handle(QueryUserRole request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);


        query = query.ApplyStringFilters(request);

        return query.ToListAsync(cancellationToken);
    }
}