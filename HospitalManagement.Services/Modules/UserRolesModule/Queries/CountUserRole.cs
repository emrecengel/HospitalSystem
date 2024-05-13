using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.UserRolesModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.UserRolesModule.Queries;

public sealed class CountUserRole : IRequest<long>, ICountableRequest
{
    internal Func<IQueryable<UserRole>, IQueryable<UserRole>>? Query { get; set; }
    public string? Filter { get; set; }

    public CountUserRole ByQuery(Func<IQueryable<UserRole>, IQueryable<UserRole>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleCountUserRole(
    IRepository<UserRole> repository) : IRequestHandler<CountUserRole, long>
{
    public Task<long> Handle(CountUserRole request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);

        query = query.ApplyStringFilters(request);

        return query.LongCountAsync(cancellationToken);
    }
}