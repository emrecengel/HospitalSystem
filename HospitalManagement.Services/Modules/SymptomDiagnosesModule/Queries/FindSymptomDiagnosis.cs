using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.SymptomDiagnosesModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.SymptomDiagnosesModule.Queries;

public sealed class FindSymptomDiagnosis : IRequest<SymptomDiagnosis?>, IFilterableRequest
{
    internal int? SymptomId { get; set; }
    internal int? DiagnosisId { get; set; }
    internal Func<IQueryable<SymptomDiagnosis>, IQueryable<SymptomDiagnosis>>? Query { get; set; }

    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public SortBy? SortBy { get; set; }


    public FindSymptomDiagnosis ById(int symptomId, int diagnosisId)
    {
        SymptomId = symptomId;
        DiagnosisId = diagnosisId;
        return this;
    }


    public FindSymptomDiagnosis ByQuery(Func<IQueryable<SymptomDiagnosis>, IQueryable<SymptomDiagnosis>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleFindSymptomDiagnosis(
    IRepository<SymptomDiagnosis> repository) : IRequestHandler<FindSymptomDiagnosis, SymptomDiagnosis?>
{
    public Task<SymptomDiagnosis?> Handle(FindSymptomDiagnosis request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);

        if (request.DiagnosisId.HasValue)
            query = query.Where(x =>
                x.SymptomId == request.SymptomId.Value && x.DiagnosisId == request.DiagnosisId.Value);


        query = query.ApplyStringFilters(request);

        return query.FirstOrDefaultAsync(cancellationToken);
    }
}