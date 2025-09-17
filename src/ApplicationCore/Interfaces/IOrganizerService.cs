using ApplicationCore.Entities.EventAggregate;

namespace ApplicationCore.Interfaces;

public interface IOrganizerService
{
    Task<int> CreateEventAsync(int organizerId, string title, string description, DateTime date, string pictureUri, EventRoleInfo roleInfo);
    Task<IReadOnlyList<Event>> GetEventsByOrganizerAsync(int organizerId);
    Task<IReadOnlyList<Application>> GetApplicationsForEventAsync(int eventId);
    Task ReviewApplicationAsync(int eventId, int applicationId, ApplicationStatus status);
}
 