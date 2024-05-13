using AutoMapper;
using HospitalManagement.Services.Modules.SymptomDiagnosesModule.Commands;
using HospitalManagement.Services.Modules.SymptomDiagnosesModule.Models;

namespace HospitalManagement.Services.Modules.SymptomDiagnosesModule.Maps;

internal class MapCreateSymptomDiagnosis : Profile
{
    public MapCreateSymptomDiagnosis()
    {
        CreateMap<CreateSymptomDiagnosis, SymptomDiagnosis>()
            .ConvertUsing<MapCreateSymptomDiagnosisConverter>()
            ;
    }
}

internal class MapCreateSymptomDiagnosisConverter : ITypeConverter<CreateSymptomDiagnosis, SymptomDiagnosis>
{
    public SymptomDiagnosis Convert(CreateSymptomDiagnosis source, SymptomDiagnosis destination,
        ResolutionContext context)
    {
        return new SymptomDiagnosis
        {
            SymptomId = source.SymptomId,
            DiagnosisId = source.DiagnosisId
        };
    }
}