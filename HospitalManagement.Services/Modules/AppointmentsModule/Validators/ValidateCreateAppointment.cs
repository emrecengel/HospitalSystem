using FluentValidation;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.AppointmentsModule.Commands;
using HospitalManagement.Services.Modules.DoctorsModule.Models;
using HospitalManagement.Services.Modules.PatientsModule.Models;
using HospitalManagement.Services.Resources;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.AppointmentsModule.Validators;

internal class ValidateCreateAppointment : AbstractValidator<CreateAppointment>
{
    public ValidateCreateAppointment(IRepository<Doctor> doctorRepository, IRepository<Patient> patientRepository)
    {
        RuleFor(x => x.DoctorId)
            .MustAsync(async (doctorId, cancellationToken) =>
            {
                return await doctorRepository.Query.AnyAsync(x => x.Id == doctorId, cancellationToken);
            })
            .WithMessage(Messages.Appointment_Validation_DoctorNotFound);

        RuleFor(x => x.PatientId)
            .MustAsync(async (patientId, cancellationToken) =>
            {
                return await patientRepository.Query.AnyAsync(x => x.Id == patientId, cancellationToken);
            })
            .WithMessage(Messages.Appointment_Validation_PatientNotFound);

        RuleFor(x => x.AppointmentOn)
            .NotEmpty()
            .WithMessage(Messages.Appointment_Validation_DateIsRequired);

        RuleFor(x => x.Reason)
            .NotEmpty()
            .WithMessage(Messages.Appointment_Validation_ReasonIsRequired);

        RuleFor(x => x.Duration)
            .NotEmpty()
            .WithMessage(Messages.Appointment_Validation_DurationIsRequired);
    }
}