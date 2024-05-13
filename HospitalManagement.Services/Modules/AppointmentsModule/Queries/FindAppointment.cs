using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.AppointmentsModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.AppointmentsModule.Queries;

public sealed class FindAppointment : IRequest<Appointment?>, IFilterableRequest
{
    internal int? Id { get; set; }
    internal Func<IQueryable<Appointment>, IQueryable<Appointment>>? Query { get; set; }

    internal int? DoctorId { get; set; }

    internal int? PatientId { get; set; }
    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public SortBy? SortBy { get; set; }


    public FindAppointment ByPatientId(int value)
    {
        PatientId = value;
        return this;
    }

    public FindAppointment ByDoctorId(int value)
    {
        DoctorId = value;
        return this;
    }

    public FindAppointment ById(int value)
    {
        Id = value;
        return this;
    }


    public FindAppointment ByQuery(Func<IQueryable<Appointment>, IQueryable<Appointment>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleFindAppointment(
    IRepository<Appointment> repository) : IRequestHandler<FindAppointment, Appointment?>
{
    public Task<Appointment?> Handle(FindAppointment request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);

        if (request.Id.HasValue) query = query.Where(x => x.Id == request.Id.Value);

        if (request.PatientId.HasValue) query = query.Where(x => x.PatientId == request.PatientId.Value);

        if (request.DoctorId.HasValue) query = query.Where(x => x.DoctorId == request.DoctorId.Value);


        query = query.ApplyStringFilters(request);

        return query.FirstOrDefaultAsync(cancellationToken);
    }
}