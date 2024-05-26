using HospitalManagement.Services.Modules.DiagnosesModule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagement.Services.Modules.DiagnosesModule.Builders;

internal class BuildDiagnosisEntity : IEntityTypeConfiguration<Diagnosis>
{
    public void Configure(EntityTypeBuilder<Diagnosis> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();

        builder.HasMany(x => x.Symptoms)
            .WithOne(x => x.Diagnosis)
            .HasForeignKey(x => x.DiagnosisId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("diagnoses");
    }
}