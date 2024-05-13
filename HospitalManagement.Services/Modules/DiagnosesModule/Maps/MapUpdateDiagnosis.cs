using AutoMapper;
using HospitalManagement.Services.Modules.DiagnosesModule.Commands;
using HospitalManagement.Services.Modules.DiagnosesModule.Models;

namespace HospitalManagement.Services.Modules.DiagnosesModule.Maps;

internal class MapUpdateDiagnosis : Profile
{
    public MapUpdateDiagnosis()
    {
        CreateMap<UpdateDiagnosis, Diagnosis>()
            .ConvertUsing<MapUpdateDiagnosisConverter>()
            ;
    }
}

internal class MapUpdateDiagnosisConverter : ITypeConverter<UpdateDiagnosis, Diagnosis>
{
    public Diagnosis Convert(UpdateDiagnosis source, Diagnosis destination, ResolutionContext context)
    {
        return new Diagnosis
        {
            Name = source.Name
        };
    }
}