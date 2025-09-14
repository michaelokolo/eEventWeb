using Microsoft.Extensions.Configuration;
namespace ApplicationCore.Constants;

public class AuthorizationConstants
{
    public static string GetDefaultPassword(IConfiguration configuration) => configuration["Authorization:DefaultPassword"]!;
}
