using ApplicationCore.Entities.EventAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages.Organizer;

public class ApplicationDetailsModel : PageModel
{
    private readonly IOrganizerDashboardViewModelService _dashboardService;
    private readonly ILogger<ApplicationDetailsModel> _logger;

    public ApplicationDetailsModel(IOrganizerDashboardViewModelService dashboardService, ILogger<ApplicationDetailsModel> logger)
    {
        _dashboardService = dashboardService;
        _logger = logger;
    }

    [BindProperty(SupportsGet = true)]
    public int EventId { get; set; }

    [BindProperty(SupportsGet = true)]
    public int ApplicationId { get; set; }

    public ApplicationViewModel? Application { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var application = await _dashboardService.GetApplicationByIdAsync(EventId, ApplicationId);
        if (application == null)
        {
            return NotFound();
        }
        Application = application;
        return Page();
    }

    public async Task<IActionResult> OnPostReviewAsync(ApplicationStatus status)
    {
        await _dashboardService.ReviewApplicationAsync(EventId, ApplicationId, status);
        return RedirectToPage("Applications", new { EventId });
    }
}
