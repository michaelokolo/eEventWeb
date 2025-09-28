using ApplicationCore.Entities.EventAggregate;
using Web.ViewModels;

namespace Web.Interfaces;

public interface IOrganizerDashboardViewModelService
{
    Task<List<EventItemViewModel>> GetEventsAsync(string organizerId);
    Task<List<ApplicationViewModel>> GetApplicationsAsync(int eventId, string organizerId);
    Task ReviewApplicationAsync(int eventId, int applicationId, ApplicationStatus status);
    Task<int> CreateEventAsync(string organizerId, CreateEventViewModel model);
    Task<ApplicationViewModel?> GetApplicationByIdAsync(int eventId, int applicationId, string organizerId);
    Task<EventItemViewModel> GetEventByIdAsync(int eventId, string organizerId);
}
