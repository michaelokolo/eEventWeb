using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ViewModels;
using Web.Services;
using Web.Interfaces;
using System.Threading.Tasks;
using Shared.Authorization;

namespace Web.Pages;

[AllowAnonymous]
public class IndexModel : PageModel
{
    private readonly IEventViewModelService _eventViewModelService;

    public IndexModel(IEventViewModelService eventViewModelService)
    {
        _eventViewModelService = eventViewModelService;
    }

    public required EventIndexViewModel EventModel { get; set; } = new EventIndexViewModel();

    public async Task<IActionResult> OnGet()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            if (User.IsInRole(Constants.Roles.ORGANIZERS))
            {
                return RedirectToPage("/Organizer/Dashboard");
            }
            else if (User.IsInRole(Constants.Roles.FREELANCERS))
            {
                return RedirectToPage("/Freelancer/Dashboard");
            }
        }

        EventModel = await _eventViewModelService.GetEvents();
        return Page();
    }
}
