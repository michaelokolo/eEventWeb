using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.ViewModels;
using Web.Services;
using Web.Interfaces;
using System.Threading.Tasks;

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

    public async Task OnGet()
    {
        EventModel = await _eventViewModelService.GetEvents();
    }
}
