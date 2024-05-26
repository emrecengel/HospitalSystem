using HospitalManagement.Services.Modules.SymptomDiagnosesModule.Models;

namespace HospitalManagement.Services.Modules.SymptomsModule.Models;

public class Symptom
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<SymptomDiagnosis> Diagnoses { get; set; }
}