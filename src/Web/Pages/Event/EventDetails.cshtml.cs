using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Web.Pages.Event;

[AllowAnonymous]
public class EventDetailsModel : PageModel
{
    private readonly IEventViewModelService _eventViewModelService;

    public EventItemViewModel? Event { get; private set; }

    public EventDetailsModel(IEventViewModelService eventViewModelService)
    {
        _eventViewModelService = eventViewModelService;
    }
    public async Task<IActionResult> OnGetAsync(int id)
    {
        Event = await _eventViewModelService.GetEventByIdAsync(id);
        if (Event == null)
        {
            return NotFound();
        }
        return Page();
    }
}
