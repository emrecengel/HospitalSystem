using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.UsersModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.UsersModule.Queries;

public sealed class CountUser : IRequest<long>, ICountableRequest
{
    internal Func<IQueryable<User>, IQueryable<User>>? Query { get; set; }
    public string? Filter { get; set; }

    public CountUser ByQuery(Func<IQueryable<User>, IQueryable<User>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleCountUser(
    IRepository<User> repository) : IRequestHandler<CountUser, long>
{
    public Task<long> Handle(CountUser request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);

        query = query.ApplyStringFilters(request);

        return query.LongCountAsync(cancellationToken);
    }
}