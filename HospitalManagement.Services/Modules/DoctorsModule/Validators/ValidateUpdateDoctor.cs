using FluentValidation;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.DoctorsModule.Commands;
using HospitalManagement.Services.Modules.DoctorsModule.Models;
using HospitalManagement.Services.Resources;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.DoctorsModule.Validators;

internal class ValidateUpdateDoctor : AbstractValidator<UpdateDoctor>
{
    public ValidateUpdateDoctor(IRepository<Doctor> repository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) =>
            {
                return await repository.Query.AnyAsync(x => x.Id == id, cancellationToken);
            })
            .WithMessage(Messages.Doctor_Validation_DoctorNotFound);

        RuleFor(x => x.FirstName).NotEmpty().WithMessage(Messages.Doctor_Validation_FistNameIsRequired);
        RuleFor(x => x.LastName).NotEmpty().WithMessage(Messages.Doctor_Validation_LastNameIsRequired);
        RuleFor(x => x.Specialization).NotEmpty().WithMessage(Messages.Doctor_Validation_SpecializationIsRequired);
        RuleFor(x => x.Availability).NotEmpty().WithMessage(Messages.Doctor_Validation_AvailabilityIsRequired);
    }
}