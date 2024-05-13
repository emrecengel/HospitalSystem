using FluentValidation;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.RolesModule.Models;
using HospitalManagement.Services.Modules.UsersModule.Commands;
using HospitalManagement.Services.Modules.UsersModule.Models;
using HospitalManagement.Services.Resources;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.UsersModule.Validators
{
    internal class ValidateAssignRole: AbstractValidator<AssignRoleToUser>
    {
        public ValidateAssignRole(IRepository<User> repository, IRepository<Role> roleRepository)
        {

            RuleFor(x => x.UserId)
                .MustAsync(async (id, cancellationToken) =>
                {
                    return await repository.Query.AnyAsync(x => x.Id == id, cancellationToken);
                })
                .WithMessage(Messages.User_Validation_UserNotFound);

            RuleFor(x => x.RoleId)
                .MustAsync(async (id, cancellationToken) =>
                {
                    return await roleRepository.Query.AnyAsync(x => x.Id == id, cancellationToken);
                })
                .WithMessage(Messages.User_Validation_UserNotFound);


            RuleFor(x => x.UserId).NotEmpty().WithMessage(Messages.User_Validation_UserIdIsRequired);
            RuleFor(x => x.RoleId).NotEmpty().WithMessage(Messages.User_Validation_RoleIdIsRequired);
        }
    }
}
