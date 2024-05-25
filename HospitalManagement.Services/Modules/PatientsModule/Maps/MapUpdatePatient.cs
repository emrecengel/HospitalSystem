using AutoMapper;
using HospitalManagement.Services.Modules.PatientsModule.Commands;
using HospitalManagement.Services.Modules.PatientsModule.Models;

namespace HospitalManagement.Services.Modules.PatientsModule.Maps;

internal class MapUpdatePatient : Profile
{
    public MapUpdatePatient()
    {
        CreateMap<UpdatePatient, Patient>()
            .ConvertUsing<MapUpdatePatientConverter>();
    }
}

internal class MapUpdatePatientConverter : ITypeConverter<UpdatePatient, Patient>
{
    public Patient Convert(UpdatePatient source, Patient destination, ResolutionContext context)
    {
        destination ??= new Patient();

        destination.FirstName = source.FirstName;
        destination.LastName = source.LastName;
        destination.PhoneNumber = source.PhoneNumber;
        destination.EmailAddress = source.EmailAddress;
        destination.Address = source.Address;
        destination.BloodType = source.BloodType;
        destination.DateOfBirth = source.DateOfBirth;
        destination.UserId = source.UserId;
        destination.Gender = source.Gender;

        return destination;
    }
}
