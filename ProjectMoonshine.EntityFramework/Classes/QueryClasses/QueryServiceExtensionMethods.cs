using Microsoft.Extensions.DependencyInjection;

namespace ProjectMoonshine.EntityFramework.Classes.QueryClasses
{
    public static class QueryServiceExtensionMethods
    {
        public static IServiceCollection AddQueryService(this IServiceCollection services)
        {
            //    services.AddTransient<TableNameLookup>()
            //      .AddTransient<DatabaseMetadata>();
            return services;
        }
    }
}
