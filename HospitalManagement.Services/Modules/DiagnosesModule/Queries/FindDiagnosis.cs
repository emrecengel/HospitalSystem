using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.DiagnosesModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.DiagnosesModule.Queries;

public sealed class FindDiagnosis : IRequest<Diagnosis?>, IFilterableRequest
{
    internal int? Id { get; set; }
    internal Func<IQueryable<Diagnosis>, IQueryable<Diagnosis>>? Query { get; set; }

    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public SortBy? SortBy { get; set; }


    public FindDiagnosis ById(int value)
    {
        Id = value;
        return this;
    }


    public FindDiagnosis ByQuery(Func<IQueryable<Diagnosis>, IQueryable<Diagnosis>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleFindDiagnosis(
    IRepository<Diagnosis> repository) : IRequestHandler<FindDiagnosis, Diagnosis?>
{
    public Task<Diagnosis?> Handle(FindDiagnosis request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);

        if (request.Id.HasValue) query = query.Where(x => x.Id == request.Id.Value);


        query = query.ApplyStringFilters(request);

        return query.FirstOrDefaultAsync(cancellationToken);
    }
}