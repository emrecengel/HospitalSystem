using HospitalManagement.Services.Modules.SymptomDiagnosesModule.Models;

namespace HospitalManagement.Services.Modules.DiagnosesModule.Models;

public class Diagnosis
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<SymptomDiagnosis> Symptoms { get; set; }
}