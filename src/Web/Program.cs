using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Web.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllersWithViews();

if (builder.Environment.IsDevelopment())
{
    Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);
}

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
});

builder.Services.AddCookieSettings();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Lax;
    });


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

// Add custom services
builder.Services.AddCoreServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);

// Add memory cache services
builder.Services.AddMemoryCache();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSassCompiler();


var app = builder.Build();

app.Logger.LogInformation("App created...");

app.Logger.LogInformation("Seeding Database...");

using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var eventAppContext = scopedProvider.GetRequiredService<EventAppContext>();
        await EventAppContextSeed.SeedAsync(eventAppContext, app.Logger);

        var userManager = scopedProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var identityContext = scopedProvider.GetRequiredService<AppIdentityDbContext>();
        await AppIdentityDbContextSeed.SeedAsync(identityContext, userManager, roleManager);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Logger.LogInformation("Application started successfully.");
app.Run();




//dotnet ef migrations add InitialIdentityModel --context AppIdentityDbContext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj -o Identity/Migrations
//dotnet ef database update --context AppIdentityDbContext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj




//dotnet ef migrations add InitialEventModel --context EventAppContext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj -o Data/Migrations
//dotnet ef database update --context EventAppContext -p ../Infrastructure/Infrastructure.csproj -s Web.csproj