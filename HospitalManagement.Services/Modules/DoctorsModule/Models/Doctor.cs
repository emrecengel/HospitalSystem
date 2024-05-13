using HospitalManagement.Services.Modules.AppointmentsModule.Models;
using HospitalManagement.Services.Modules.UsersModule.Models;

namespace HospitalManagement.Services.Modules.DoctorsModule.Models;

public class Doctor
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Specialization { get; set; }
    public int? UserId { get; set; }
    public string Availability { get; set; }
    internal List<Appointment>? Appointments { get; set; }
    internal User? User { get; set; }
}