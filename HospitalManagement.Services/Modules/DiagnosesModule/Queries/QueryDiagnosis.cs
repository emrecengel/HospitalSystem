using Azure.Core;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.DiagnosesModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.DiagnosesModule.Queries;

public sealed class QueryDiagnosis : IRequest<List<Diagnosis>>, IFilterableRequest, IPageableRequest
{
    internal Func<IQueryable<Diagnosis>, IQueryable<Diagnosis>>? Query { get; set; }
    public int[]? SymptomIds { get; set; }

    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public SortBy? SortBy { get; set; }
    public int? PageSize { get; set; }
    public int PageIndex { get; set; }

    public QueryDiagnosis ByQuery(Func<IQueryable<Diagnosis>, IQueryable<Diagnosis>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleQueryDiagnosis(
    IRepository<Diagnosis> repository) : IRequestHandler<QueryDiagnosis, List<Diagnosis>>
{
    public async Task<List<Diagnosis>> Handle(QueryDiagnosis request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);


        if (request.SymptomIds != null)
            query = query.Where(x => x.Symptoms.Any(y => request.SymptomIds.Contains(y.SymptomId))).OrderByDescending(x => x.Symptoms.Count()).Include(x=> x.Symptoms);

        query = query.ApplyStringFilters(request);

        var diagnosis = await query.ToListAsync(cancellationToken);

        if (request.SymptomIds is { Length: > 0 })
        {
            // var symptomIdSet = new HashSet<int>(request.SymptomIds);
            //

            diagnosis.ForEach(x =>
            {
                var symptomIdSet = x.Symptoms.Where(y=> request.SymptomIds.Contains(y.SymptomId));

                if (x.Symptoms is { Count: > 0 })
                {
                  
                    x.SymptomMatchPercentage = ((double)symptomIdSet.Count() / x.Symptoms.Count()) * 100;
                }
                else
                {
                    x.SymptomMatchPercentage = 0;
                }
            });
        }



        return diagnosis;
    }
}