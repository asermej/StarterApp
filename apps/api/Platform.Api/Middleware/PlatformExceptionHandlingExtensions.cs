using Microsoft.AspNetCore.Builder;

namespace Platform.Api.Middleware;

public static class PlatformExceptionHandlingExtensions
{
    public static IApplicationBuilder UsePlatformExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<PlatformExceptionHandlingMiddleware>();
    }
}
