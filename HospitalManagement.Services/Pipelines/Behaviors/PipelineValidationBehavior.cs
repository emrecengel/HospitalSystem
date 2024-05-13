using System.Diagnostics;
using FluentValidation;
using MediatR;

namespace HospitalManagement.Services.Pipelines.Behaviors;

[DebuggerStepThrough]
internal sealed class PipelineValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var failures = validators
            .Select(async v => await v.ValidateAsync(request, cancellationToken)).Select(x => x.Result)
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Any()) throw new ValidationException(failures);

        return next();
    }
}