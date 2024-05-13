using AutoMapper;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.AppointmentsModule.Models;
using MediatR;

namespace HospitalManagement.Services.Modules.AppointmentsModule.Commands;

public class CreateAppointment : IRequest<Appointment>
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentOn { get; set; }
    public string Reason { get; set; }
    public TimeSpan Duration { get; set; }
}

internal sealed class HandleCreateAppointment(
    IRepository<Appointment> repository,
    IMapper mapper) : IRequestHandler<CreateAppointment, Appointment>
{
    public Task<Appointment> Handle(CreateAppointment request, CancellationToken cancellationToken)
    {
        return repository.Create(mapper.Map<Appointment>(request));
    }
}