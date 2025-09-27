using ApplicationCore.Entities.EventAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Ardalis.GuardClauses;

namespace ApplicationCore.Services;

public class FreelancerService : IFreelancerService
{
    private readonly IRepository<Event> _eventRepository;
    private readonly IAppLogger<FreelancerService> _logger;

    public FreelancerService(IRepository<Event> eventRepository, IAppLogger<FreelancerService> logger)
    {
        _eventRepository = eventRepository;
        _logger = logger;
    }

    public async Task<IReadOnlyList<Event>> GetAvailableEventsAsync(string freelancerId)
    {
        var spec = new AvailableEventsForFreelancerSpecification(freelancerId);
        var events = await _eventRepository.ListAsync(spec);
        return events;
    }

    public async Task<int> ApplyToEventAsync(int eventId, string freelancerId, string message)
    {
        Guard.Against.NullOrEmpty(freelancerId, nameof(freelancerId));
        Guard.Against.NegativeOrZero(eventId, nameof(eventId));
        var eventEntity = await _eventRepository.GetByIdAsync(eventId);
        if (eventEntity == null)
        {
            _logger.LogWarning("Event with ID {EventId} not found", eventId);
            throw new KeyNotFoundException($"Event with ID {eventId} not found.");
        }
          var application = eventEntity.Apply(freelancerId, message);
        await _eventRepository.UpdateAsync(eventEntity);
        _logger.LogInFormation("Freelancer {FreelancerId} applied to Event {EventId}", freelancerId, eventId);
        return application.Id;
    }

    public async Task<IReadOnlyList<Event>> GetEventsByIdsAsync(IEnumerable<int> eventIds)
    {
        var spec = new EventsByIdsSpecification(eventIds);
        return await _eventRepository.ListAsync(spec);
    }

    public async Task<IReadOnlyList<Application>> GetApplicationsByFreelancerAsync(string freelancerId)
    {
        Guard.Against.NullOrEmpty(freelancerId, nameof(freelancerId));
        var spec = new EventsWithApplicationsByFreelancerSpecification(freelancerId);
        var events = await _eventRepository.ListAsync(spec);

        var applications = events
            .SelectMany(e => e.Applications)
            .Where(a => a.FreelancerId == freelancerId)
            .ToList();
        return applications;
    }
}
