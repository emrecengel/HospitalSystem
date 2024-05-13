using FluentValidation;
using HospitalManagement.Services.Modules.UsersModule.Commands;
using HospitalManagement.Services.Resources;

namespace HospitalManagement.Services.Modules.UsersModule.Validators;

internal class ValidateCreateUser : AbstractValidator<CreateUser>
{
    public ValidateCreateUser()
    {
        RuleFor(x => x.EmailAddress).NotEmpty().WithMessage(Messages.User_Validation_UserNameIsRequired);
        RuleFor(x => x.Password).NotEmpty().WithMessage(Messages.User_Validation_PasswordIsRequired);
    }
}