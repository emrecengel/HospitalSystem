using HospitalManagement.Services.Modules.DiagnosesModule.Models;
using HospitalManagement.Services.Modules.DoctorsModule.Models;
using HospitalManagement.Services.Modules.PatientsModule.Models;

namespace HospitalManagement.Services.Modules.AppointmentsModule.Models;

public class Appointment
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentOn { get; set; }
    public string Reason { get; set; }
    public TimeSpan Duration { get; set; }
    public int? OutComeDiagnosisId { get; set; }
    internal Diagnosis? OutComeDiagnosis { get; set; }
    internal Patient Patient { get; set; }
    internal Doctor Doctor { get; set; }
}