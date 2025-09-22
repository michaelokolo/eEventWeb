using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Authorization;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages.Organizer;

[Authorize(Roles = Constants.Roles.ORGANIZERS)]
public class CreateEventModel : PageModel
{
    private readonly ILogger<CreateEventModel> _logger;
    private readonly IOrganizerDashboardViewModelService _dashboardService;

    public CreateEventModel(ILogger<CreateEventModel> logger, IOrganizerDashboardViewModelService dashboardService)
    {
        _logger = logger;
        _dashboardService = dashboardService;
    }
    [BindProperty]
    public CreateEventViewModel Event { get; set; } = new();


    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        string? organizerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        await _dashboardService.CreateEventAsync(organizerId!, Event);
        return RedirectToPage("Dashboard");
    }
}
