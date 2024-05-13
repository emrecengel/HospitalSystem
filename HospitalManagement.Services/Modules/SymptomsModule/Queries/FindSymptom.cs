using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.SymptomsModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.SymptomsModule.Queries;

public sealed class FindSymptom : IRequest<Symptom?>, IFilterableRequest
{
    internal int? Id { get; set; }
    internal Func<IQueryable<Symptom>, IQueryable<Symptom>>? Query { get; set; }

    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public SortBy? SortBy { get; set; }


    public FindSymptom ById(int value)
    {
        Id = value;
        return this;
    }


    public FindSymptom ByQuery(Func<IQueryable<Symptom>, IQueryable<Symptom>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleFindSymptom(
    IRepository<Symptom> repository) : IRequestHandler<FindSymptom, Symptom?>
{
    public Task<Symptom?> Handle(FindSymptom request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);

        if (request.Id.HasValue) query = query.Where(x => x.Id == request.Id.Value);


        query = query.ApplyStringFilters(request);

        return query.FirstOrDefaultAsync(cancellationToken);
    }
}