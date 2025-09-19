using ApplicationCore.Entities.EventAggregate;
using ApplicationCore.Entities.OrganizerAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System.Collections.Generic;

namespace ApplicationCore.Services;

public class OrganizerService : IOrganizerService
{
    private readonly IRepository<Event> _eventRepository;
    private readonly IAppLogger<OrganizerService> _logger;

    public OrganizerService(IRepository<Event> eventRepository, IAppLogger<OrganizerService> logger)
    {
        _eventRepository = eventRepository;
        _logger = logger;

    }

    public async Task<IReadOnlyList<Event>> GetEventsByOrganizerAsync(string organizerId)
    {
        var spec = new EventsByOrganizerSpecification(organizerId);
        return await _eventRepository.ListAsync(spec);
    }

    public async Task<int> CreateEventAsync(string organizerId, string title, string description, DateTime date, string pictureUri, EventRoleInfo roleInfo)
    {
        var newEvent = new Event(title, description, date, pictureUri, organizerId, roleInfo);
        await _eventRepository.AddAsync(newEvent);
        _logger.LogInFormation("Event created with ID {EventId} by Organizer {OrganizerId}", newEvent.Id, organizerId);
        return newEvent.Id;
    }

    public async Task<IReadOnlyList<Application>> GetApplicationsForEventAsync(int eventId)
    {
        var spec = new EventWithApplicationsSpecification(eventId);
        var eventEntity = await _eventRepository.FirstOrDefaultAsync(spec);
        return eventEntity?.Applications.ToList() ?? new List<Application>();
    }

    public async Task ReviewApplicationAsync(int eventId, int applicationId, ApplicationStatus status)
    {
        var spec = new EventWithApplicationsSpecification(eventId);
        var eventEntity = await _eventRepository.FirstOrDefaultAsync(spec);
        if (eventEntity == null)
        {
            _logger.LogWarning("Event with ID {EventId} not found", eventId);
            throw new KeyNotFoundException($"Event with ID {eventId} not found.");
        }
        try
        {
            eventEntity.ReviewApplication(applicationId, status);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Application with ID {ApplicationId} not found in Event {EventId}: {Message}", applicationId, eventId, ex.Message);
            throw new KeyNotFoundException($"Application with ID {applicationId} not found in Event {eventId}.");
        }
        await _eventRepository.UpdateAsync(eventEntity);
        _logger.LogInFormation("Application with ID {ApplicationId} for Event {EventId} reviewed with status {Status}", applicationId, eventId, status);
    }
}



// Next Steps:
//Dashboard Page
//•	Displays a list of events for the organizer.
//•	Button to create a new event.
//•	Button/link to view applications for each event.
//Create Event Page
//•	Form for event details.
//•	On submit, calls OrganizerService.CreateEventAsync.
//Applications Page
//•	Displays applications for a selected event.
//•	Allows organizer to review (approve/reject/withdraw) applications.
