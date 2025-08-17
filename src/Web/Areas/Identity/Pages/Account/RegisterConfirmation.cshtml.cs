using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Areas.Identity.Pages.Account;

[AllowAnonymous]
public class RegisterConfirmationModel : PageModel
{
    public string? Email { get; set; }
    public void OnGet(string? email = null)
    {
        Email = email;
    }
}
