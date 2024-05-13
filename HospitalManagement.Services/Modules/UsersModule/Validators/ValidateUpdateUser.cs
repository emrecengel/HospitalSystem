using FluentValidation;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.UsersModule.Commands;
using HospitalManagement.Services.Modules.UsersModule.Models;
using HospitalManagement.Services.Resources;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.UsersModule.Validators;

internal class ValidateUpdateUser : AbstractValidator<UpdateUser>
{
    public ValidateUpdateUser(IRepository<User> repository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) =>
            {
                return await repository.Query.AnyAsync(x => x.Id == id, cancellationToken);
            })
            .WithMessage(Messages.User_Validation_UserNotFound);

        RuleFor(x => x.EmailAddress).NotEmpty().WithMessage(Messages.User_Validation_UserNameIsRequired);
        RuleFor(x => x.Password).NotEmpty().WithMessage(Messages.User_Validation_PasswordIsRequired);
    }
}