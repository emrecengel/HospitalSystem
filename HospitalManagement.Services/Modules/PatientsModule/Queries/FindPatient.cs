using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.PatientsModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.PatientsModule.Queries;

public sealed class FindPatient : IRequest<Patient?>, IFilterableRequest
{
    internal int? Id { get; set; }
    internal Func<IQueryable<Patient>, IQueryable<Patient>>? Query { get; set; }

    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public SortBy? SortBy { get; set; }


    public FindPatient ById(int value)
    {
        Id = value;
        return this;
    }


    public FindPatient ByQuery(Func<IQueryable<Patient>, IQueryable<Patient>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleFindPatient(
    IRepository<Patient> repository) : IRequestHandler<FindPatient, Patient?>
{
    public Task<Patient?> Handle(FindPatient request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);

        if (request.Id.HasValue) query = query.Where(x => x.Id == request.Id.Value);


        query = query.ApplyStringFilters(request);

        return query.FirstOrDefaultAsync(cancellationToken);
    }
}