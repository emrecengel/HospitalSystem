using FluentValidation;
using HospitalManagement.Services.Modules.SymptomsModule.Models;
using HospitalManagement.Services.Resources;

namespace HospitalManagement.Services.Modules.SymptomsModule.Validators;

internal class ValidateCreateSymptom : AbstractValidator<Symptom>
{
    public ValidateCreateSymptom()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(Messages.Symptom_Validation_NameIsRequired);
    }
}