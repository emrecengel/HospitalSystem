using AutoMapper;
using HospitalManagement.Services.Modules.DoctorsModule.Commands;
using HospitalManagement.Services.Modules.DoctorsModule.Models;

namespace HospitalManagement.Services.Modules.DoctorsModule.Maps;

internal class UpdateMapDoctor : Profile
{
    public UpdateMapDoctor()
    {
        CreateMap<UpdateDoctor, Doctor>()
            .ConvertUsing<UpdateMapDoctorConverter>()
            ;
    }
}

internal class UpdateMapDoctorConverter : ITypeConverter<UpdateDoctor, Doctor>
{
    public Doctor Convert(UpdateDoctor source, Doctor destination, ResolutionContext context)
    {
        destination ??= new Doctor();

        destination.FirstName = source.FirstName;
        destination.LastName = source.LastName;
        destination.Specialization = source.Specialization;
        destination.Availability = source.Availability;
        destination.UserId = source.UserId;

        return destination;
    }
}