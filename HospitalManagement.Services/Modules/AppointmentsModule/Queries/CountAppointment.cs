using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Extensions;
using HospitalManagement.Services.Modules.AppointmentsModule.Models;
using HospitalManagement.Services.RequestTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.AppointmentsModule.Queries;

public sealed class CountAppointment : IRequest<long>, ICountableRequest
{
    internal Func<IQueryable<Appointment>, IQueryable<Appointment>> Query { get; set; }
    public string Filter { get; set; }

    public CountAppointment ByQuery(Func<IQueryable<Appointment>, IQueryable<Appointment>> value)
    {
        Query = value;

        return this;
    }
}

internal sealed class HandleCountAppointment(
    IRepository<Appointment> repository) : IRequestHandler<CountAppointment, long>
{
    public Task<long> Handle(CountAppointment request, CancellationToken cancellationToken)
    {
        var query = repository.Query;

        if (request.Query != null) query = request.Query(query);

        query = query.ApplyStringFilters(request);

        return query.LongCountAsync(cancellationToken);
    }
}