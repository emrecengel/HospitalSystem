using FluentValidation;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.AppointmentsModule.Commands;
using HospitalManagement.Services.Modules.AppointmentsModule.Models;
using HospitalManagement.Services.Resources;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.AppointmentsModule.Validators;

internal class ValidateDeleteAppointment : AbstractValidator<DeleteAppointment>
{
    public ValidateDeleteAppointment(IRepository<Appointment> repository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) =>
            {
                return await repository.Query.AnyAsync(x => x.Id == id, cancellationToken);
            })
            .WithMessage(Messages.Appointment_Validation_AppointmentNotFound);
    }
}