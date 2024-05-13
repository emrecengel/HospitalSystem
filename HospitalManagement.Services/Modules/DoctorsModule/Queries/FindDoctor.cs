using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.DoctorsModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.DoctorsModule.Queries;

public sealed class FindDoctor : IRequest<Doctor?>, IFilterableRequest
{
    internal int? Id { get; set; }
    internal Func<IQueryable<Doctor>, IQueryable<Doctor>>? Query { get; set; }

    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public SortBy? SortBy { get; set; }


    public FindDoctor ById(int value)
    {
        Id = value;
        return this;
    }


    public FindDoctor ByQuery(Func<IQueryable<Doctor>, IQueryable<Doctor>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleFindDoctor(
    IRepository<Doctor> repository) : IRequestHandler<FindDoctor, Doctor?>
{
    public Task<Doctor?> Handle(FindDoctor request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);

        if (request.Id.HasValue) query = query.Where(x => x.Id == request.Id.Value);


        query = query.ApplyStringFilters(request);

        return query.FirstOrDefaultAsync(cancellationToken);
    }
}