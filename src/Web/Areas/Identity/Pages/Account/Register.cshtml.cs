using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using static Shared.Authorization.Constants;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using ApplicationCore.Interfaces;
using System.Text.Encodings.Web;


namespace Web.Areas.Identity.Pages.Account;

[AllowAnonymous]
public class RegisterModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<RegisterModel> _logger;
    private readonly IEmailSender _emailSender;
    private readonly IUserProfileService _userProfileService;

    public RegisterModel(
        UserManager<ApplicationUser> userManager,
        ILogger<RegisterModel> logger,
        IEmailSender emailSender,
        IUserProfileService userProfileService)
    {
        _userManager = userManager;
        _logger = logger;
        _emailSender = emailSender;
        _userProfileService = userProfileService;
    }

    [BindProperty]
    public required InputModel Input { get; set; }

    public string? ReturnUrl { get; set; }

    public List<string> AvailableRoles { get; } = new()
    {
        Roles.ORGANIZERS,
        Roles.FREELANCERS
    };


    public class InputModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string? Role { get; set; }
    }

    public void OnGet(string? returnUrl = null)
    {
        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        ReturnUrl = returnUrl ?? Url.Content("~/");

        if (!AvailableRoles.Contains(Input.Role!))
        {
            ModelState.AddModelError("Input.Role", "Invalid role selection.");
            return Page();
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = new ApplicationUser { UserName = Input?.Email, Email = Input?.Email };
        var result = await _userManager.CreateAsync(user, Input?.Password!);

        if (!result.Succeeded)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }

        var roleResult = await _userManager.AddToRoleAsync(user, Input?.Role!);
        if (!roleResult.Succeeded)
        {
            foreach (var error in roleResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            await _userManager.DeleteAsync(user);
            return Page();
        }

        if (Input?.Role == Roles.FREELANCERS)
        {
            await _userProfileService.CreateFreelancerAsync(user.Id, user.UserName ?? user.Email ?? "");
        }
        else if (Input?.Role == Roles.ORGANIZERS)
        {
            await _userProfileService.CreateOrganizerAsync(user.Id, user.UserName ?? user.Email ?? "");
        }

        _logger.LogInformation("User created a new account with password.");

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var callbackUrl = Url.Page(
            "/Account/ConfirmEmail",
            pageHandler: null,
            values: new { userId = user.Id, code = code!},
            protocol: Request.Scheme);

        if (callbackUrl == null)
        {
            ModelState.AddModelError(string.Empty, "Error generating confirmation link.");
            return Page();
        }
        
        await _emailSender.SendEmailAsync(Input!.Email!, "Confirm your email",
            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        return RedirectToPage("/Account/RegisterConfirmation", new { email = Input.Email });

    }
}
