using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.RolesModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.RolesModule.Queries;

public sealed class FindRole : IRequest<Role?>, IFilterableRequest
{
    internal int? Id { get; set; }
    internal Func<IQueryable<Role>, IQueryable<Role>>? Query { get; set; }

    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public SortBy? SortBy { get; set; }


    public FindRole ById(int value)
    {
        Id = value;
        return this;
    }


    public FindRole ByQuery(Func<IQueryable<Role>, IQueryable<Role>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleFindRole(
    IRepository<Role> repository) : IRequestHandler<FindRole, Role?>
{
    public Task<Role?> Handle(FindRole request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);

        if (request.Id.HasValue) query = query.Where(x => x.Id == request.Id.Value);


        query = query.ApplyStringFilters(request);

        return query.FirstOrDefaultAsync(cancellationToken);
    }
}