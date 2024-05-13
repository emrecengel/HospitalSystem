using FluentValidation;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.SymptomsModule.Commands;
using HospitalManagement.Services.Modules.SymptomsModule.Models;
using HospitalManagement.Services.Resources;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.SymptomsModule.Validators;

internal class ValidateDeleteSymptom : AbstractValidator<DeleteSymptom>
{
    public ValidateDeleteSymptom(IRepository<Symptom> repository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) =>
            {
                return await repository.Query.AnyAsync(x => x.Id == id, cancellationToken);
            })
            .WithMessage(Messages.Symptom_Validation_SymptomNotFound);
    }
}