using AutoMapper;
using HospitalManagement.Services.Modules.DoctorsModule.Commands;
using HospitalManagement.Services.Modules.DoctorsModule.Models;

namespace HospitalManagement.Services.Modules.DoctorsModule.Maps
{
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
            return new Doctor
            {
                FirstName = source.FirstName,
                LastName = source.LastName,
                Specialization = source.Specialization,
                Availability = source.Availability
            };
        }
    }
}
