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
        destination ??= new Appointment();

        destination.PatientId = source.PatientId;
        destination.DoctorId = source.DoctorId;
        destination.AppointmentOn = source.AppointmentOn;
        destination.Reason = source.Reason;
        destination.Duration = source.Duration;
        destination.OutComeDiagnosisId = source.OutComeDiagnosisId;


        return destination;
    }
}