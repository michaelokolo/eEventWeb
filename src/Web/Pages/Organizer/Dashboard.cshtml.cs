using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Services;
using Web.ViewModels;
using Ardalis.GuardClauses;
using Shared.Authorization;
using Web.Interfaces;

namespace Web.Pages.Organizer;

[Authorize(Roles = Constants.Roles.ORGANIZERS)]
public class DashboardModel : PageModel
{
    private readonly IOrganizerDashboardViewModelService _dashboardService;

    public DashboardModel(IOrganizerDashboardViewModelService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public List<EventItemViewModel> Events { get; set; } = new();
    public async Task OnGetAsync()
    {
        string? userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        Guard.Against.NullOrEmpty(userId, nameof(userId), "User ID claim is missing.");
        Events = await _dashboardService.GetEventsAsync(userId);
    }
}
