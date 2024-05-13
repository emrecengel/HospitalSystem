using AutoMapper;
using HospitalManagement.Services.Modules.PatientsModule.Commands;
using HospitalManagement.Services.Modules.PatientsModule.Models;

namespace HospitalManagement.Services.Modules.PatientsModule.Maps;

internal class MapUpdatePatient : Profile
{
    public MapUpdatePatient()
    {
        CreateMap<UpdatePatient, Patient>()
            .ConvertUsing<MapUpdatePatientConverter>()
            ;
    }
}

internal class MapUpdatePatientConverter : ITypeConverter<UpdatePatient, Patient>
{
    public Patient Convert(UpdatePatient source, Patient destination, ResolutionContext context)
    {
        return new Patient
        {
            FirstName = source.FirstName,
            LastName = source.LastName,
            PhoneNumber = source.PhoneNumber,
            EmailAddress = source.EmailAddress,
            Address = source.Address,
            BloodType = source.BloodType,
            DateOfBirth = source.DateOfBirth
        };
    }
}