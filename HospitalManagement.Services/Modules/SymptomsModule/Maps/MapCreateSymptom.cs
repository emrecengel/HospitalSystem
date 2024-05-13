using AutoMapper;
using HospitalManagement.Services.Modules.SymptomsModule.Commands;
using HospitalManagement.Services.Modules.SymptomsModule.Models;

namespace HospitalManagement.Services.Modules.SymptomsModule.Maps;

internal class MapCreateSymptom : Profile
{
    public MapCreateSymptom()
    {
        CreateMap<CreateSymptom, Symptom>()
            .ConvertUsing<MapCreateSymptomConverter>()
            ;
    }
}

internal class MapCreateSymptomConverter : ITypeConverter<CreateSymptom, Symptom>
{
    public Symptom Convert(CreateSymptom source, Symptom destination, ResolutionContext context)
    {
        return new Symptom
        {
            Name = source.Name
        };
    }
}