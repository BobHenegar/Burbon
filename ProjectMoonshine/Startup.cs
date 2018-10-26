using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectMoonshine.Classes.QueryClasses;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Helpers;
using ProjectMoonshine.Helpers;
using ProjectMoonshine.Managers;

namespace ProjectMoonshine
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new ConfigurationBuilder()
#if DEBUG
                .AddJsonFile($"{Environment.CurrentDirectory}\\appsettings.json", optional: true)
#else
                .AddJsonFile($"{Environment.CurrentDirectory}\\appsettings.Development.json", optional: false)
#endif
                .Build();
            services.AddDbContextPool<ApplicationDbContext>(options =>
                options
                    .UseSqlServer(config.GetConnectionString("Database"),
                        b => b.MigrationsAssembly("ProjectMoonshine.EntityFramework")));

            //Setup Dependency injection
            services.AddDependencies();
            services.AddQueryService();

            //Configure Cookie Policy Options
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                // supresses the Newtonsoft.Json.JsonSerializationExceptions for cycles
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            if (Assembly.GetEntryAssembly().FullName.StartsWith("ProjectMoonshine,"))
            {
                // Uncomment to debug Design time issues
                //if (!System.Diagnostics.Debugger.IsAttached)
                //{
                //    System.Diagnostics.Debugger.Launch();
                //}
                using (var context = services.BuildServiceProvider().GetService<ApplicationDbContext>())
                {
                    if (context != null)
                    {
                        if (!context.Database.GetAppliedMigrations().ToList().Any(a => a == "20181010203923_InitialCreate"))
                        {
                            context.Database.EnsureDeleted();
                        }
                        var migrationsToApply = context.Database.GetMigrations().ToList().Except(context.Database.GetAppliedMigrations().ToList());
                        if (migrationsToApply.Any())
                        {
                            context.Database.Migrate();
                            context.RunMigrations(migrationsToApply.ToList());
                            context.SaveChanges();
                        }
                    }
                }
            }
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            //Hide and log errors only in when not deployed
#if !DEBUG
            //app.UseErrorLogging();
#endif
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id:guid?}");
            });
        }
    }
}
