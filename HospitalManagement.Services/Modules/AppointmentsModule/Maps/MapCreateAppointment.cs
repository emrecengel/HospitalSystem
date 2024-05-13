using AutoMapper;
using HospitalManagement.Services.Modules.AppointmentsModule.Commands;
using HospitalManagement.Services.Modules.AppointmentsModule.Models;

namespace HospitalManagement.Services.Modules.AppointmentsModule.Maps;

internal class MapCreateAppointment : Profile
{
    public MapCreateAppointment()
    {
        CreateMap<CreateAppointment, Appointment>()
            .ConvertUsing<MapCreateAppointmentConverter>()
            ;
    }
}

internal class MapCreateAppointmentConverter : ITypeConverter<CreateAppointment, Appointment>
{
    public Appointment Convert(CreateAppointment source, Appointment destination, ResolutionContext context)
    {
        return new Appointment
        {
            PatientId = source.PatientId,
            DoctorId = source.DoctorId,
            AppointmentOn = source.AppointmentOn,
            Reason = source.Reason,
            Duration = source.Duration
        };
    }
}