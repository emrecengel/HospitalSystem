using System.Security.Claims;
using System.Security.Principal;

namespace HospitalManagement.Services.DatabaseRepository.Extensions;

internal static class PrincipalExtensions

{
    public static Guid UserId(this IPrincipal principal)
    {
        var claimsPrincipal = principal as ClaimsPrincipal;
        var userId = claimsPrincipal?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        return string.IsNullOrWhiteSpace(userId) ? Guid.Empty : new Guid(userId);
    }

    public static string? UserIdString(this IPrincipal? principal)
    {
        if (principal == null) return default;

        var claimsPrincipal = principal as ClaimsPrincipal;

        var userId = claimsPrincipal?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        return userId;
    }
}