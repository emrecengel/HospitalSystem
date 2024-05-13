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

        builder.ToTable("symptom_diagnoses");
    }
}