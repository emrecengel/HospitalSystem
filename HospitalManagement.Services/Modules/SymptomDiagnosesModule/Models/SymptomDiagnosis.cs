using HospitalManagement.Services.Modules.DiagnosesModule.Models;
using HospitalManagement.Services.Modules.SymptomsModule.Models;

namespace HospitalManagement.Services.Modules.SymptomDiagnosesModule.Models;

public class SymptomDiagnosis
{
    public int SymptomId { get; set; }
    public int DiagnosisId { get; set; }

    public Symptom Symptom { get; set; }

    public Diagnosis Diagnosis { get; set; }
}