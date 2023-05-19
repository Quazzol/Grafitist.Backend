using Microsoft.AspNetCore.Builder;

namespace Grafitist.Common.Middleware;

public static class ConcurrentUserAuthorizationMiddlewareExtensions
{
    public static IApplicationBuilder UseConcurrentUserAuthorization(this IApplicationBuilder builder) =>
        builder.UseMiddleware<ConcurrentUserAuthorizationMiddleware>();
}
