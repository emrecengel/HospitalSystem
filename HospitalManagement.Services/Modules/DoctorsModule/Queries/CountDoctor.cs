using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.DoctorsModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.DoctorsModule.Queries;

public sealed class CountDoctor : IRequest<long>, ICountableRequest
{
    internal Func<IQueryable<Doctor>, IQueryable<Doctor>>? Query { get; set; }
    public string? Filter { get; set; }

    public CountDoctor ByQuery(Func<IQueryable<Doctor>, IQueryable<Doctor>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleCountDoctor(
    IRepository<Doctor> repository) : IRequestHandler<CountDoctor, long>
{
    public Task<long> Handle(CountDoctor request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);

        query = query.ApplyStringFilters(request);

        return query.LongCountAsync(cancellationToken);
    }
}