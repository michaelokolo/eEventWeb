using Web.ViewModels;

namespace Web.Interfaces;

public interface IFreelancerDashboardViewModelService
{
    Task<List<EventItemViewModel>> GetAvailableEventsAsync(string freelancerId);
    Task<List<ApplicationViewModel>> GetApplicationsAsync(string freelancerId);
    Task<int> ApplyToEventAsync(int eventId, string freelancerId);
}
