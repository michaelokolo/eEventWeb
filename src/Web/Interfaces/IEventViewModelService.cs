using Web.ViewModels;

namespace Web.Interfaces;

public interface IEventViewModelService
{
    Task<EventIndexViewModel> GetEvents();
    Task<EventItemViewModel?> GetEventByIdAsync(int id);

    Task<OrganizerViewModel> GetOrganizerAsync(int organizerId);
}
