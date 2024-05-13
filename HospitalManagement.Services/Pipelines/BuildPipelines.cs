using HospitalManagement.Services.Pipelines.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagement.Services.Pipelines;

internal static class BuildPipelines
{
    public static IServiceCollection UsePipelines(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PipelineValidationBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PipelineMediatrLoggingBehavior<,>));

        return services;
    }
}