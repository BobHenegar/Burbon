using Microsoft.AspNetCore.Builder;

namespace ProjectMoonshine.Middleware
{
    public static class MiddlewareExtensionMethods
    {
        public static IApplicationBuilder UseErrorLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorLoggingMiddleware>(builder);
        }
    }
}
