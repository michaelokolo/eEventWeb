using ApplicationCore.Entities.EventAggregate;

namespace ApplicationCore.Interfaces;

public interface IFreelancerService
{
    Task<IReadOnlyList<Event>> GetAvailableEventsAsync(string freelancerId);
    Task<int> ApplyToEventAsync(int eventId, string freelancerId);
    Task<IReadOnlyList<Application>> GetApplicationsByFreelancerAsync(string freelancerId);
}
