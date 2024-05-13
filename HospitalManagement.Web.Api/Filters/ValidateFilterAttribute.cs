using FluentValidation;
using FluentValidation.Results;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HospitalManagement.Web.Api.Filters;

public sealed class ValidateFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is ValidationException)
        {
            context.ModelState.Clear();
            var exception = context.Exception as ValidationException;

            foreach (var error in exception?.Errors ?? new List<ValidationFailure>())
                context.ModelState.AddModelError(error.PropertyName.Camelize(), error.ErrorMessage);
        }


        if (!context.ModelState.IsValid)
        {
            context.Exception = null;
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}