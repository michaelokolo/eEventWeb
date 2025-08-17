using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Areas.Identity.Pages.Account;

[AllowAnonymous]
public class ConfirmEmailModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;

    public ConfirmEmailModel(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [TempData]
    public string StatusMessage { get; set; } = string.Empty;


    public async Task<IActionResult> OnGetAsync(string? userId, string? code)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code))
        {
            StatusMessage = "Invalid confirmation link.";
            return Page();
        }

        var user = await _userManager.FindByIdAsync(userId);
        if ( user == null)
        {
            StatusMessage = "User not found.";
            return Page();
        }

        var result = await _userManager.ConfirmEmailAsync(user, code);
        StatusMessage = result.Succeeded
            ? "Thank you for confirming your email! You can now log in."
            : "Error confirming your email.";
        return Page();
    }
}
