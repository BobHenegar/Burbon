using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Helpers;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.Middleware
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider services;

        public ErrorLoggingMiddleware(RequestDelegate next, IApplicationBuilder appBuilder)
        {
            _next = next;
            services = appBuilder.ApplicationServices;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await LogErrorMessageAsync(ex, context.Request.Path.Value).ConfigureAwait(false);
            }
        }
        private async Task LogErrorMessageAsync(Exception ex, string path)
        {
            await Task.Run(async () =>
            {
                using (var scope = services.CreateScope())
                {
                    using (var context = scope.ServiceProvider.GetService<ApplicationDbContext>())
                    {
                        var s = new StackTrace(ex);
                        var thisAsm = Assembly.GetExecutingAssembly();
                        var methodName = s.GetFrames().Select(f => f.GetMethod()).First(m => m.Module.Assembly == thisAsm).Name;
                        var log = new ErrorLog()
                        {
                            ExceptionType = ex.GetType().FullName,
                            Message = ex.RecursivelyFormatException(),
                            Controller = ex.Source,
                            Method = methodName
                        };
                        await context.ErrorLogs.AddAsync(log);
                        await context.SaveChangesAsync();
                    }
                }
            }).ConfigureAwait(false);
        }
    }
}
