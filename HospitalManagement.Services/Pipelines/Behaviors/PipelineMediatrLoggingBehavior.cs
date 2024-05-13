using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HospitalManagement.Services.Pipelines.Behaviors;

[DebuggerStepThrough]
internal sealed class PipelineMediatrLoggingBehavior<TRequest, TResponse>(
    ILogger<TRequest> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var response = await next();
            stopWatch.Stop();

            if (response == null) return default;

            using (logger.BeginScope(new Dictionary<string, object>
                   {
                       { "Level", "Mediatr" }, { "@MediatrRequest", request }, { "@MediatrResponse", response }
                   }))
            {
                logger.LogInformation("Mediatr: {Name} ({Elapsed}ms)", request.GetType().Name,
                    stopWatch.ElapsedMilliseconds);
            }

            return response;
        }
        catch (Exception exception)
        {
            using (logger.BeginScope(
                       new Dictionary<string, object> { { "Level", "Mediatr" }, { "@MediatrRequest", request } }))
            {
                logger.LogError(exception, "Mediatr: {Name}", request.GetType().Name);
            }

            throw;
        }
    }
}