using FluentValidation;
using HospitalManagement.Services.Modules.RolesModule.Models;
using HospitalManagement.Services.Resources;

namespace HospitalManagement.Services.Modules.RolesModule.Validators;

internal class ValidateCreateRole : AbstractValidator<Role>
{
    public ValidateCreateRole()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(Messages.Role_Validation_NameIsRequired);
        RuleFor(x => x.Description).NotEmpty().WithMessage(Messages.Role_Validation_DescriptionIsRequired);
    }
}