using ApplicationCore.Entities.EventAggregate;

namespace ApplicationCore.Interfaces;

public interface IOrganizerService
{
    Task<int> CreateEventAsync(string organizerId, string title, string description, DateTime date, string pictureUri, EventRoleInfo roleInfo);
    Task<IReadOnlyList<Event>> GetEventsByOrganizerAsync(string organizerId);
    Task<IReadOnlyList<Application>> GetApplicationsForEventAsync(int eventId);
    Task ReviewApplicationAsync(int eventId, int applicationId, ApplicationStatus status);
    Task<Application?> GetApplicationByIdAsync(int eventId, int applicationId);
    Task<Event?> GetEventByOrganizerAndIdAsync(string organizerId, int eventId);
}
 