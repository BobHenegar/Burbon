using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProjectMoonshine.EntityFramework
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Uncomment to debug OnModelCreating
            //if (!System.Diagnostics.Debugger.IsAttached)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}
            var config = new ConfigurationBuilder()
                .AddJsonFile($"{Environment.CurrentDirectory}\\appsettings.json", optional: false)
                .Build();
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(config.GetConnectionString("Database"),
                b => b.MigrationsAssembly("ProjectMoonshine.EntityFramework"));
            Environment.SetEnvironmentVariable("isDesignTime", "true");
            return new ApplicationDbContext(builder.Options);
        }
    }
}
