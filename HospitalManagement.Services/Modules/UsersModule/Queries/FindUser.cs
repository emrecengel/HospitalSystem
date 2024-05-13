using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.UsersModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.UsersModule.Queries;

public sealed class FindUser : IRequest<User?>, IFilterableRequest
{
    internal int? Id { get; set; }
    internal Func<IQueryable<User>, IQueryable<User>>? Query { get; set; }

    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public SortBy? SortBy { get; set; }


    public FindUser ById(int value)
    {
        Id = value;
        return this;
    }


    public FindUser ByQuery(Func<IQueryable<User>, IQueryable<User>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleFindUser(
    IRepository<User> repository) : IRequestHandler<FindUser, User?>
{
    public Task<User?> Handle(FindUser request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);

        if (request.Id.HasValue) query = query.Where(x => x.Id == request.Id.Value);


        query = query.ApplyStringFilters(request);

        return query.FirstOrDefaultAsync(cancellationToken);
    }
}