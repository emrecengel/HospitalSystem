using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.UserRolesModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.UserRolesModule.Queries;

public sealed class FindUserRole : IRequest<UserRole?>, IFilterableRequest
{
    internal int? UserId { get; set; }
    internal int? RoleId { get; set; }
    internal Func<IQueryable<UserRole>, IQueryable<UserRole>>? Query { get; set; }

    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public SortBy? SortBy { get; set; }


    public FindUserRole ById(int userId, int roleId)
    {
        UserId = userId;
        RoleId = roleId;
        return this;
    }


    public FindUserRole ByQuery(Func<IQueryable<UserRole>, IQueryable<UserRole>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleFindUserRole(
    IRepository<UserRole> repository) : IRequestHandler<FindUserRole, UserRole?>
{
    public Task<UserRole?> Handle(FindUserRole request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);


        if (request.UserId.HasValue)
            query = query.Where(x =>
                x.UserId == request.UserId.Value && x.RoleId == request.RoleId.Value);


        query = query.ApplyStringFilters(request);

        return query.FirstOrDefaultAsync(cancellationToken);
    }
}