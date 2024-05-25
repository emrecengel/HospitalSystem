using HospitalManagement.Services.Modules.PatientsModule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagement.Services.Modules.PatientsModule.Builders;

internal class BuildPatientEntity : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();

        builder.Property(x => x.Address).IsRequired();
        builder.Property(x => x.PhoneNumber).IsRequired();
        builder.Property(x => x.EmailAddress).IsRequired();
        builder.Property(x => x.BloodType).IsRequired();
        builder.Property(x => x.Gender).IsRequired();
        builder.Property(x => x.DateOfBirth).IsRequired();
        builder.Property(x => x.UserId).IsRequired(false);

        builder.HasMany(x => x.Appointments).WithOne().HasForeignKey(x => x.PatientId).IsRequired();
        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).IsRequired(false);

        builder.ToTable("patients");
    }
}