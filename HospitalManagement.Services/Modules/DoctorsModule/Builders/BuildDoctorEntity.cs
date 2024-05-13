using HospitalManagement.Services.Modules.DoctorsModule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagement.Services.Modules.DoctorsModule.Builders;

internal class BuildDoctorEntity : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();
        builder.Property(x => x.Specialization).IsRequired();
        builder.Property(x => x.UserId).IsRequired(false);
        builder.Property(x => x.Availability).IsRequired();

        builder.HasMany(x => x.Appointments).WithOne().HasForeignKey(x => x.DoctorId).IsRequired();
        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).IsRequired(false);

        builder.ToTable("doctors");
    }
}