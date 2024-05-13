using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.UsersModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.UsersModule.Queries;

public sealed class QueryUser : IRequest<List<User>>, IFilterableRequest, IPageableRequest
{
    internal Func<IQueryable<User>, IQueryable<User>>? Query { get; set; }

    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public SortBy? SortBy { get; set; }
    public int? PageSize { get; set; }
    public int PageIndex { get; set; }

    public QueryUser ByQuery(Func<IQueryable<User>, IQueryable<User>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleQueryUser(
    IRepository<User> repository) : IRequestHandler<QueryUser, List<User>>
{
    public Task<List<User>> Handle(QueryUser request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);


        query = query.ApplyStringFilters(request);

        return query.ToListAsync(cancellationToken);
    }
}