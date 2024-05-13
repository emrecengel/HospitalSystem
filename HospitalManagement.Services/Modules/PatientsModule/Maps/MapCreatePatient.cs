using AutoMapper;
using HospitalManagement.Services.Modules.PatientsModule.Commands;
using HospitalManagement.Services.Modules.PatientsModule.Models;

namespace HospitalManagement.Services.Modules.PatientsModule.Maps;

internal class MapCreatePatient : Profile
{
    public MapCreatePatient()
    {
        CreateMap<CreatePatient, Patient>()
            .ConvertUsing<MapCreatePatientConverter>()
            ;
    }
}

internal class MapCreatePatientConverter : ITypeConverter<CreatePatient, Patient>
{
    public Patient Convert(CreatePatient source, Patient destination, ResolutionContext context)
    {
        return new Patient
        {
            FirstName = source.FirstName,
            LastName = source.LastName,
            PhoneNumber = source.PhoneNumber,
            EmailAddress = source.EmailAddress,
            Address = source.Address,
            BloodType = source.BloodType,
            DateOfBirth = source.DateOfBirth,
            Gender = source.Gender
        };
    }
}
