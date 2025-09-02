using Microsoft.EntityFrameworkCore;
using Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;

namespace Infrastructure;

public static class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        // use real database for now
        // Add IdentityDbContext with SQL Server
        services.AddDbContext<EventAppContext>(c => 
            c.UseSqlServer(configuration.GetConnectionString("EventConnection")));

        // Add Identity DbContext
        services.AddDbContext<AppIdentityDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
    }
}
