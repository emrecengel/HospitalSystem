using FluentValidation;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.DiagnosesModule.Commands;
using HospitalManagement.Services.Modules.DiagnosesModule.Models;
using HospitalManagement.Services.Resources;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.DiagnosesModule.Validators;

internal class ValidateUpdateDiagnosis : AbstractValidator<UpdateDiagnosis>
{
    public ValidateUpdateDiagnosis(IRepository<Diagnosis> repository)
    {

        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) =>
            {
                return await repository.Query.AnyAsync(x => x.Id == id, cancellationToken);
            })
            .WithMessage(Messages.Diagnosis_Validation_DiagnosisNotFound);


        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(Messages.Diagnosis_Validation_NameIsRequired)
            .MaximumLength(100)
            .WithMessage(Messages.Diagnosis_Validation_NameLength);
    }
}