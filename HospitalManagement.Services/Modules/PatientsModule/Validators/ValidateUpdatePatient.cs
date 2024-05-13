using FluentValidation;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Modules.PatientsModule.Commands;
using HospitalManagement.Services.Modules.PatientsModule.Models;
using HospitalManagement.Services.Resources;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Services.Modules.PatientsModule.Validators;

internal class ValidateUpdatePatient : AbstractValidator<UpdatePatient>
{
    public ValidateUpdatePatient(IRepository<Patient> repository)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) =>
            {
                return await repository.Query.AnyAsync(x => x.Id == id, cancellationToken);
            })
            .WithMessage(Messages.Patient_Validation_PatientNotFound);

        RuleFor(x => x.FirstName).NotEmpty().WithMessage(Messages.Patient_Validation_FirstNameIsRequired);
        RuleFor(x => x.LastName).NotEmpty().WithMessage(Messages.Patient_Validation_LastNameIsRequired);
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage(Messages.Patient_Validation_PhoneNumberIsRequired);
        RuleFor(x => x.EmailAddress).EmailAddress().NotEmpty().WithMessage(Messages.Patient_Validation_EmailAddressIsRequired);
        RuleFor(x => x.Address).NotEmpty().WithMessage(Messages.Patient_Validation_AddressIsRequired);
        RuleFor(x => x.BloodType).NotEmpty().WithMessage(Messages.Patient_Validation_BloodTypeIsRequired);
        RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage(Messages.Patient_Validation_DateOfBirthIsRequired);
    }
}