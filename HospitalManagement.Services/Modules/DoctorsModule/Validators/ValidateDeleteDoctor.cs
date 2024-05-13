using FluentValidation;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.DoctorsModule.Models;
using HospitalManagement.Services.Resources;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.DoctorsModule.Validators;

internal class ValidateDeleteDoctor : AbstractValidator<Doctor>
{
    public ValidateDeleteDoctor(IRepository<Doctor> repository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) =>
            {
                return await repository.Query.AnyAsync(x => x.Id == id, cancellationToken);
            })
            .WithMessage(Messages.Doctor_Validation_DoctorNotFound);
    }
}