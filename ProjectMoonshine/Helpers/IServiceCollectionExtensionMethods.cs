using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using ProjectMoonshine.Managers;

namespace ProjectMoonshine.Helpers
{
    public static class IServiceCollectionExtensionMethods
    {
        public static void AddDependencies(this IServiceCollection services)
        {

            services
                .AddTransient<PersonManager>()
                .AddTransient<AddressManager>()
                .AddTransient<CredentialManager>()
                .AddTransient<EmailManager>()
                .AddTransient<PhoneManager>()
                .AddTransient<CredentialManager>()
                .AddTransient<ExamManager>()
                .AddTransient<ExamScoreManager>()
                .AddTransient<EventRegistrationManager>()
                .AddTransient<EventManager>()
                .AddTransient<FileUploadManager>()
                //.AddTransient<QueryManager>()
                .AddTransient<RequirementManager>();

            services
                .AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }
    }
}
