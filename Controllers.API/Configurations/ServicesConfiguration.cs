using ELI.Data.Repositories;
using ELI.Data.Repositories.Auth;
using ELI.Data.Repositories.Main;
using ELI.Domain.Contracts;
using ELI.Domain.Contracts.Auth;
using ELI.Domain.Contracts.Main;
using ELI.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace ELI.API.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<IShowRepository, ShowRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IActivationRepository, ActivationRepository>();
            services.AddScoped<IActivationTypeRepository, ActivationTypeRepository>();
            services.AddScoped<ILookupTableRepository, LookupTableRepository>();
            services.AddScoped<IELIRepository, ELIRepository>();
            services.AddScoped<ISduactivationRespository, SduactivationRespository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IQualifierRepository, QualifierRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddScoped<ILeadsRepository, LeadRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();

            return services;
        }
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IELIService, ELIService>();
            services.AddScoped<IELIAuthService, ELIAuthService>();
            services.AddScoped<IEmailSender, EmailSender>();
            return services;
        }
        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            return services;
        }
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            var corsBuilder = new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", corsBuilder.Build());
            });

            return services;
        }
    }
}