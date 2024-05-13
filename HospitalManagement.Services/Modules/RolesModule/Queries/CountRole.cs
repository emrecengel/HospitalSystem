using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.RolesModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.RolesModule.Queries;

public sealed class CountRole : IRequest<long>, ICountableRequest
{
    internal Func<IQueryable<Role>, IQueryable<Role>>? Query { get; set; }
    public string? Filter { get; set; }

    public CountRole ByQuery(Func<IQueryable<Role>, IQueryable<Role>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleCountRole(
    IRepository<Role> repository) : IRequestHandler<CountRole, long>
{
    public Task<long> Handle(CountRole request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);

        query = query.ApplyStringFilters(request);

        return query.LongCountAsync(cancellationToken);
    }
}