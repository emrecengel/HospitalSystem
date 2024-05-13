using HospitalManagement.Services.Modules.AppointmentsModule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagement.Services.Modules.AppointmentsModule.Builders;

internal class BuildAppointmentEntity : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PatientId).IsRequired();
        builder.Property(x => x.DoctorId).IsRequired();
        builder.Property(x => x.AppointmentOn).IsRequired();
        builder.Property(x => x.Reason).IsRequired();
        builder.Property(x => x.Duration).IsRequired();
        builder.Property(x => x.OutComeDiagnosisId).IsRequired(false);

        builder.HasOne(x => x.OutComeDiagnosis).WithMany().HasForeignKey(x => x.OutComeDiagnosisId).IsRequired(false);
        builder.HasOne(x => x.Patient).WithMany().HasForeignKey(x => x.PatientId).IsRequired();
        builder.HasOne(x => x.Doctor).WithMany().HasForeignKey(x => x.DoctorId).IsRequired();

        builder.ToTable("appointments");
    }
}