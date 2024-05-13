using AutoMapper;
using HospitalManagement.Services.Modules.AppointmentsModule.Commands;
using HospitalManagement.Services.Modules.AppointmentsModule.Models;

namespace HospitalManagement.Services.Modules.AppointmentsModule.Maps;

internal class MapUpdateAppointment : Profile
{
    public MapUpdateAppointment()
    {
        CreateMap<UpdateAppointment, Appointment>()
            .ConvertUsing<MapUpdateAppointmentConverter>()
            ;
    }
}

internal class MapUpdateAppointmentConverter : ITypeConverter<UpdateAppointment, Appointment>
{
    public Appointment Convert(UpdateAppointment source, Appointment destination, ResolutionContext context)
    {
        return new Appointment
        {
            PatientId = source.PatientId,
            DoctorId = source.DoctorId,
            AppointmentOn = source.AppointmentOn,
            Reason = source.Reason,
            Duration = source.Duration,
            OutComeDiagnosisId = source.OutComeDiagnosisId
        };
    }
}