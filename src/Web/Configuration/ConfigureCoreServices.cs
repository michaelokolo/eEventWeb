using ApplicationCore.Interfaces;
using Infrastructure.Services;
using static Infrastructure.Services.EmailSender;

namespace Web.Configuration;

public static class ConfigureCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
        services.AddTransient<IEmailSender, EmailSender>();

        return services;
    }
}