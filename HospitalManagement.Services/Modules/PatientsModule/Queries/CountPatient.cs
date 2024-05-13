using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.PatientsModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.PatientsModule.Queries;

public sealed class CountPatient : IRequest<long>, ICountableRequest
{
    internal Func<IQueryable<Patient>, IQueryable<Patient>>? Query { get; set; }
    public string? Filter { get; set; }

    public CountPatient ByQuery(Func<IQueryable<Patient>, IQueryable<Patient>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleCountPatient(
    IRepository<Patient> repository) : IRequestHandler<CountPatient, long>
{
    public Task<long> Handle(CountPatient request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);

        query = query.ApplyStringFilters(request);

        return query.LongCountAsync(cancellationToken);
    }
}