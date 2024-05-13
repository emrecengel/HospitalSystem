using AutoMapper;
using HospitalManagement.Services.Modules.DoctorsModule.Commands;
using HospitalManagement.Services.Modules.DoctorsModule.Models;

namespace HospitalManagement.Services.Modules.DoctorsModule.Maps;

internal class MapCreateDoctor : Profile
{
    public MapCreateDoctor()
    {
        CreateMap<CreateDoctor, Doctor>()
            .ConvertUsing<MapCreateDoctorConverter>()
            ;
    }
}

internal class MapCreateDoctorConverter : ITypeConverter<CreateDoctor, Doctor>
{
    public Doctor Convert(CreateDoctor source, Doctor destination, ResolutionContext context)
    {
        return new Doctor
        {
            FirstName = source.FirstName,
            LastName = source.LastName,
            Specialization = source.Specialization,
            Availability = source.Availability
        };
    }
}
