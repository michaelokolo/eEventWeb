using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Authorization;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages.Organizer;

[Authorize(Roles = Constants.Roles.ORGANIZERS)]
public class EventDetailsModel : PageModel
{

    private readonly IOrganizerDashboardViewModelService _dashboardService;

    public EventDetailsModel(IOrganizerDashboardViewModelService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [BindProperty(SupportsGet = true)]
    public int EventId { get; set; }

    public EventItemViewModel? Event { get; set; }


    public async Task<IActionResult> OnGetAsync()
    {
        string? organizerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(organizerId))
            return Unauthorized();

        var evt = await _dashboardService.GetEventByIdAsync(EventId, organizerId);
        Event = evt;
        if (Event == null)
            return NotFound();

        return Page();
    }
}
