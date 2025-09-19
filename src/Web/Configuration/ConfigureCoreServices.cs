using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Infrastructure.Logging;
using Infrastructure.Services;
using static Infrastructure.Services.EmailSender;

namespace Web.Configuration;

public static class ConfigureCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IOrganizerService, OrganizerService>();

        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
        services.AddTransient<IEmailSender, EmailSender>();

        return services;
    }
}