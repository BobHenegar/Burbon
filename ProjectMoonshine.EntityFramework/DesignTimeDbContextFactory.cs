using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProjectMoonshine.EntityFramework
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(@"Server=.\\SQLEXPRESS;Database=ProjectMoonshine;Trusted_Connection=true",
                b => b.MigrationsAssembly("ProjectMoonshine.EntityFramework"));
            Environment.SetEnvironmentVariable("isDesignTime", "true");
            return new ApplicationDbContext(builder.Options);
        }
    }
}
