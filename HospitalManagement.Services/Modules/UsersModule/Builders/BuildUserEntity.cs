using HospitalManagement.Services.Modules.UsersModule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagement.Services.Modules.UsersModule.Builders;

internal class BuildUserEntity : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.EmailAddress).IsRequired();
        builder.Property(x => x.Password).IsRequired();

        builder.ToTable("users");
    }
}