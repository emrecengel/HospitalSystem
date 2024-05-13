using FluentValidation;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.DiagnosesModule.Commands;
using HospitalManagement.Services.Modules.DiagnosesModule.Models;
using HospitalManagement.Services.Modules.SymptomsModule.Models;
using HospitalManagement.Services.Resources;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.DiagnosesModule.Validators;

internal class ValidateAssignSymptomToDiagnosis : AbstractValidator<AssignSymptomToDiagnosis>
{
    public ValidateAssignSymptomToDiagnosis(IRepository<Diagnosis> repository, IRepository<Symptom> symptomRepository)
    {
        RuleFor(x => x.DiagnosisId)
            .MustAsync(async (id, cancellationToken) =>
            {
                return await repository.Query.AnyAsync(x => x.Id == id, cancellationToken);
            })
            .WithMessage(Messages.Diagnosis_Validation_DiagnosisNotFound);

        RuleFor(x => x.SymptomId)
            .MustAsync(async (id, cancellationToken) =>
            {
                return await symptomRepository.Query.AnyAsync(x => x.Id == id, cancellationToken);
            })
            .WithMessage(Messages.Symptom_Validation_SymptomNotFound);
    }
}