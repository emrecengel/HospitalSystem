using HospitalManagement.Services.Modules.SymptomDiagnosesModule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagement.Services.Modules.SymptomDiagnosesModule.Builders;

internal class BuildSymptomDiagnosisEntity : IEntityTypeConfiguration<SymptomDiagnosis>
{
    public void Configure(EntityTypeBuilder<SymptomDiagnosis> builder)
    {
        builder.HasKey(x => new { x.SymptomId, x.DiagnosisId });
        builder.Property(x => x.SymptomId).IsRequired();
        builder.Property(x => x.DiagnosisId).IsRequired();

        builder.HasOne(x => x.Symptom)
            .WithMany(x => x.Diagnoses)
            .HasForeignKey(x => x.SymptomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Diagnosis)
            .WithMany(x => x.Symptoms)
            .HasForeignKey(x => x.DiagnosisId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.ToTable("symptom_diagnoses");
    }
}