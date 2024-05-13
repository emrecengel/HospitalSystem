using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagement.Services.DatabaseRepository;

public static class BuildRepository
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        return services;
    }
}