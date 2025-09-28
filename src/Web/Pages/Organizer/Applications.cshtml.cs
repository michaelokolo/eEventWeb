using ApplicationCore.Entities.EventAggregate;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;
using Shared.Authorization;

namespace Web.Pages.Organizer;

[Authorize(Roles = Constants.Roles.ORGANIZERS)]
public class ApplicationsModel : PageModel
{
    private readonly ILogger<ApplicationsModel> _logger;
    private readonly IOrganizerDashboardViewModelService _dashboardService;
    public ApplicationsModel(ILogger<ApplicationsModel> logger, IOrganizerDashboardViewModelService dashboardService, IOrganizerService organizerService)
    {
        _logger = logger;
        _dashboardService = dashboardService;
    }

    public List<ApplicationViewModel> Applications { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public int EventId { get; set; }

    public async Task OnGetAsync()
    {
        string? organizerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(organizerId))
        {
            Applications = new List<ApplicationViewModel>();
            return;
        }
        Applications = await _dashboardService.GetApplicationsAsync(EventId, organizerId);
    }
}
