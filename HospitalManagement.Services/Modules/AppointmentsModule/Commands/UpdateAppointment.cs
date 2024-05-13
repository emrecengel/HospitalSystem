using AutoMapper;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.AppointmentsModule.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.AppointmentsModule.Commands;

public class UpdateAppointment : IRequest<Unit>
{
    internal int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentOn { get; set; }
    public string Reason { get; set; }
    public TimeSpan Duration { get; set; }
    public int? OutComeDiagnosisId { get; set; }

    public UpdateAppointment UseId(int value)
    {
        Id = value;
        return this;
    }
}

internal sealed class HandleUpdateAppointment(
    IRepository<Appointment> repository,
    IMapper mapper) : IRequestHandler<UpdateAppointment, Unit>
{
    public async Task<Unit> Handle(UpdateAppointment request, CancellationToken cancellationToken)
    {
        var query = repository.Query;
        var model = await query.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        mapper.Map(request, model);
        await repository.Update(model);
        return Unit.Value;
    }
}