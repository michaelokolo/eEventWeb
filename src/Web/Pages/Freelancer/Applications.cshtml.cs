using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Authorization;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages.Freelancer;

[Authorize(Roles = Constants.Roles.FREELANCERS)]
public class ApplicationsModel : PageModel
{
    private readonly IFreelancerDashboardViewModelService _dashboardService;

    public ApplicationsModel(IFreelancerDashboardViewModelService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public List<ApplicationViewModel> Applications { get; set; } = new();

    public async Task OnGetAsync()
    {
        string? freelancerId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (!string.IsNullOrEmpty(freelancerId))
        {
            Applications = await _dashboardService.GetApplicationsAsync(freelancerId);
        }
    }
}
