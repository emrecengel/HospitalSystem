using FluentValidation;
using HospitalManagement.Services.Modules.SymptomsModule.Commands;
using HospitalManagement.Services.Modules.SymptomsModule.Models;
using HospitalManagement.Services.Resources;

namespace HospitalManagement.Services.Modules.SymptomsModule.Validators;

internal class ValidateCreateSymptom : AbstractValidator<CreateSymptom>
{
    public ValidateCreateSymptom()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(Messages.Symptom_Validation_NameIsRequired);
    }
}