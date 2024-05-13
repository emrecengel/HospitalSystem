using HospitalManagement.Services.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AI.Assistant.Services.DataAccess;

internal sealed class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder
            .UseNpgsql(
                "server=localhost;Database=hospital_db;user id=user;password=password;port=5432;")
            .UseSnakeCaseNamingConvention();

        return new DatabaseContext(optionsBuilder.Options, null);
    }
}