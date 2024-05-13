using FluentValidation;
using HospitalManagement.Services.Modules.DoctorsModule.Commands;
using HospitalManagement.Services.Modules.DoctorsModule.Models;
using HospitalManagement.Services.Resources;

namespace HospitalManagement.Services.Modules.DoctorsModule.Validators;

internal class ValidateCreateDoctor : AbstractValidator<CreateDoctor>
{
    public ValidateCreateDoctor()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage(Messages.Doctor_Validation_FistNameIsRequired);
        RuleFor(x => x.LastName).NotEmpty().WithMessage(Messages.Doctor_Validation_LastNameIsRequired);
        RuleFor(x => x.Specialization).NotEmpty().WithMessage(Messages.Doctor_Validation_SpecializationIsRequired);
        RuleFor(x => x.Availability).NotEmpty().WithMessage(Messages.Doctor_Validation_AvailabilityIsRequired);
    }
}