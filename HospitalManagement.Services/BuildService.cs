using System.Reflection;
using FluentValidation;
using HospitalManagement.Services.DataAccess;
using HospitalManagement.Services.DatabaseRepository;
using HospitalManagement.Services.Pipelines;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagement.Services;

public sealed class ServiceOption
{
    internal string ConnectionString { get; private set; }


    public ServiceOption UseConnectionString(string value)
    {
        ConnectionString = value;
        return this;
    }
}

public static class BuildService
{
    public static IServiceCollection AddHospitalManagementService(this IServiceCollection services,
        Action<ServiceOption> value)
    {
        var options = new ServiceOption();
        value(options);

        services.AddRepository();

        services.UsePipelines();
        services.AddAutoMapper(config => { }, typeof(BuildService).Assembly);

        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(BuildService).Assembly));

        services.AddDbContext<DatabaseContext>((_, builder) =>
        {
            builder.UseNpgsql(options.ConnectionString,
                    db => { db.MigrationsAssembly(typeof(BuildService).Assembly.FullName); })
                .UseSnakeCaseNamingConvention()
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
        });


        services.AddScoped<DbContext, DatabaseContext>();


        var validatorTypes = Assembly.GetExecutingAssembly().GetTypes();
        var scanner = new AssemblyScanner(validatorTypes);
        scanner.ForEach(pair => { services.Add(ServiceDescriptor.Transient(pair.InterfaceType, pair.ValidatorType)); });

        return services;
    }
}