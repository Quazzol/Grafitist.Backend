using Grafitist.Misc.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Grafitist.Authorization;

public class HasLoggedInAuthorizationHandler : AuthorizationHandler<HasLoggedInRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasLoggedInRequirement requirement)
    {
        var claim = context.User.Claims.FirstOrDefault(q => q.Type.Equals("UserType"));
        if (claim != null && Enum.TryParse<UserType>(claim.ValueType, true, out var userType))
        {
            if (userType == UserType.Admin || userType == UserType.User)
                context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}