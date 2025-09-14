using ApplicationCore.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Shared.Authorization.Constants;

namespace Infrastructure.Identity;

public class AppIdentityDbContextSeed
{
    public static async Task SeedAsync(AppIdentityDbContext identityDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        if (identityDbContext.Database.IsSqlServer())
        {
            identityDbContext.Database.Migrate();
        }

        string[] roles = [Roles.ADMINISTRATORS, Roles.ORGANIZERS, Roles.FREELANCERS];

        foreach (var role in roles)
        {
            if(!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }


        // Seed users and assign roles
        await SeedUserWithRoleAsync(userManager, Roles.FREELANCERS, "freelancer@eevent.com", configuration);
        await SeedUserWithRoleAsync(userManager, Roles.ORGANIZERS, "organizer@eevent.com", configuration);
        await SeedUserWithRoleAsync(userManager, Roles.ADMINISTRATORS, "admin@eevent.com", configuration);

    }


    private static async Task SeedUserWithRoleAsync(UserManager<ApplicationUser> userManager, string role, string email, IConfiguration configuration)
    {
        var user = await userManager.FindByNameAsync(email);
        if (user == null)
        {
            user = new ApplicationUser { UserName = email, Email = email };
            var result = await userManager.CreateAsync(user, AuthorizationConstants.GetDefaultPassword(configuration));
            if (!result.Succeeded)
            {
                // Optionally log or throw
                return;
            }
        }

        if (!await userManager.IsInRoleAsync(user, role))
        {
            await userManager.AddToRoleAsync(user, role);
        }
    }
}