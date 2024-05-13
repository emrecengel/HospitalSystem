using AutoMapper;
using HospitalManagement.Services.Modules.DiagnosesModule.Commands;
using HospitalManagement.Services.Modules.DiagnosesModule.Models;

namespace HospitalManagement.Services.Modules.DiagnosesModule.Maps;

internal class MapCreateDiagnosis : Profile
{
    public MapCreateDiagnosis()
    {
        CreateMap<CreateDiagnosis, Diagnosis>()
            .ConvertUsing<MapCreateDiagnosisConverter>()
            ;
    }
}

internal class MapCreateDiagnosisConverter : ITypeConverter<CreateDiagnosis, Diagnosis>
{
    public Diagnosis Convert(CreateDiagnosis source, Diagnosis destination, ResolutionContext context)
    {
        return new Diagnosis
        {
            Name = source.Name
        };
    }
}


