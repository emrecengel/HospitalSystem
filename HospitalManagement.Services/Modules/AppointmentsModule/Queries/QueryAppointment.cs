using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.AppointmentsModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.AppointmentsModule.Queries;

public sealed class QueryAppointment : IRequest<List<Appointment>>, IFilterableRequest, IPageableRequest
{
    internal Func<IQueryable<Appointment>, IQueryable<Appointment>>? Query { get; set; }

    internal long? PatientId { get; set; }
    internal long? DoctorId { get; set; }

    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public SortBy? SortBy { get; set; }
    public int? PageSize { get; set; }
    public int PageIndex { get; set; }

    public QueryAppointment ByQuery(Func<IQueryable<Appointment>, IQueryable<Appointment>> value)
    {
        Query = value;

        return this;
    }

    public QueryAppointment ByPatientId(long value)
    {
        PatientId = value;

        return this;
    }

    public QueryAppointment ByDoctorId(long value)
    {
        DoctorId = value;

        return this;
    }
}

internal sealed class HandleQueryAppointment(
    IRepository<Appointment> repository) : IRequestHandler<QueryAppointment, List<Appointment>>
{
    public Task<List<Appointment>> Handle(QueryAppointment request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);


        if (request.PatientId.HasValue)
            query = query.Where(x => x.PatientId == request.PatientId);

        if (request.DoctorId.HasValue)
            query = query.Where(x => x.DoctorId == request.DoctorId);

        query = query.ApplyStringFilters(request);

        return query.ToListAsync(cancellationToken);
    }
}