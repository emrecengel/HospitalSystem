using FluentValidation;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.RolesModule.Models;
using HospitalManagement.Services.Resources;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.RolesModule.Validators;

internal class ValidateUpdateRole : AbstractValidator<Role>
{
    public ValidateUpdateRole(IRepository<Role> repository)
    {

        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) =>
            {
                return await repository.Query.AnyAsync(x => x.Id == id, cancellationToken);
            })
            .WithMessage(Messages.Role_Validation_RoleNotFound);

        RuleFor(x => x.Name).NotEmpty().WithMessage(Messages.Role_Validation_NameIsRequired);
        RuleFor(x => x.Description).NotEmpty().WithMessage(Messages.Role_Validation_DescriptionIsRequired);
    }
}