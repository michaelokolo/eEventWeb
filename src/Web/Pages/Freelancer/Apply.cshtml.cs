using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;
using Shared.Authorization;

namespace Web.Pages.Freelancer;

[Authorize(Roles = Constants.Roles.FREELANCERS)]
public class ApplyModel : PageModel
{
    private readonly IFreelancerDashboardViewModelService _dashboardService;

    public ApplyModel(IFreelancerDashboardViewModelService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [BindProperty(SupportsGet = true)]
    public int EventId { get; set; }

    [BindProperty]
    public string Message { get; set; } = "";

    public EventItemViewModel? Event { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        string? freelancerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(freelancerId)) 
        {
            return Unauthorized();
        }

        var events = await _dashboardService.GetAvailableEventsAsync(freelancerId);
        Event = events.FirstOrDefault(e => e.Id == EventId);
        if(Event == null)
            return NotFound();

        return Page();
    }


    public async Task<IActionResult> OnPostAsync()
    {
        string? freelancerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(freelancerId))
            return Unauthorized();

        await _dashboardService.ApplyToEventAsync(EventId, freelancerId, Message);
        return RedirectToPage("Applications");
    }

}
