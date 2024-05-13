using FluentValidation;
using HospitalManagement.Services.Modules.DiagnosesModule.Commands;
using HospitalManagement.Services.Resources;

namespace HospitalManagement.Services.Modules.DiagnosesModule.Validators;

internal class ValidateCreateDiagnosis : AbstractValidator<CreateDiagnosis>
{
    public ValidateCreateDiagnosis()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(Messages.Diagnosis_Validation_NameIsRequired)
            .MaximumLength(100)
            .WithMessage(Messages.Diagnosis_Validation_NameLength);
    }
}