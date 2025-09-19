using Web.ViewModels;

namespace Web.Interfaces;

public interface IOrganizerDashboardViewModelService
{
    Task<List<EventItemViewModel>> GetEventsAsync(string organizerId);
    Task<List<ApplicationViewModel>> GetApplicationsAsync(int eventId);
}
