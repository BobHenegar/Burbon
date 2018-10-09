using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.Managers;
using ProjectMoonshine.Middleware;

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

            services
                .AddTransient<IAddressManager, AddressManager>()
                .AddTransient<ICredentialManager, CredentialManager>()
                .AddTransient<IEmailManager, EmailManager>()
                .AddTransient<IPhoneManager, PhoneManager>()
                .AddTransient<ICredentialManager, CredentialManager>()
                .AddTransient<IRequirementManager, RequirementManager>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            using (var context = services.BuildServiceProvider().GetService<ApplicationDbContext>())
            {
                if (context != null)
                {
                    if (!context.Database.GetAppliedMigrations().ToList().Any(a => a == "20181009155927_InitialCreate"))
                    {
                        context.Database.EnsureDeleted();
                    }
                    var applied = context.Database.GetMigrations().ToList().Except(context.Database.GetAppliedMigrations().ToList());
                    if (applied.Any())
                    {
                        context.Database.Migrate();
                        context.SaveChanges();
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
            app.UseErrorLogging();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
