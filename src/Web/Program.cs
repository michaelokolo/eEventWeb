using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Web.Configuration;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllersWithViews();

if (builder.Environment.IsDevelopment())
{
    Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);
}

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

// Add memory cache services
builder.Services.AddMemoryCache();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

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
