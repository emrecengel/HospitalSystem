using HospitalManagement.Services.Modules.AppointmentsModule.Models;
using HospitalManagement.Services.Modules.UsersModule.Models;

namespace HospitalManagement.Services.Modules.PatientsModule.Models;

public class Patient
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? UserId { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }
    public string Address { get; set; }
    public string BloodType { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<Appointment>? Appointments { get; set; }
    public User? User { get; set; }
}