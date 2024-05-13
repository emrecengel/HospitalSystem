using HospitalManagement.Services.DatabaseRepository.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HospitalManagement.Services.DataAccess;

internal sealed class DatabaseContext(
    DbContextOptions<DatabaseContext> options,
    ILoggerFactory logger)
    : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (logger != null)
            optionsBuilder.UseLoggerFactory(logger);


        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        modelBuilder.AddChangeTracker(typeof(Guid));
    }
}