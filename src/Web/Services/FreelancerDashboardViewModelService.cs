using ApplicationCore.Interfaces;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services;

public class FreelancerDashboardViewModelService : IFreelancerDashboardViewModelService
{
    private readonly IFreelancerService _freelancerService;
    private readonly ILogger<FreelancerDashboardViewModelService> _logger;

    public FreelancerDashboardViewModelService(IFreelancerService freelancerService, ILogger<FreelancerDashboardViewModelService> logger)
    {
        _freelancerService = freelancerService;
        _logger = logger;
    }

    public async Task<List<EventItemViewModel>> GetAvailableEventsAsync(string freelancerId)
    {
        var events = await _freelancerService.GetAvailableEventsAsync(freelancerId);
        return events.Select(e => new EventItemViewModel
        {
            Id = e.Id,
            Title = e.Title,
            Date = e.Date,
            PictureUri = e.PictureUri,
            Role = e.RoleInfo?.Role,
            Location = e.RoleInfo?.Location,
            Budget = e.RoleInfo?.Budget,
            Requirements = e.RoleInfo?.Requirements?.Select(r => new RequirementViewModel
            {
                Id = r.Id,
                Description = r.Description
            }).ToList() ?? new List<RequirementViewModel>(),
        }).ToList();
    }

    public async Task<List<ApplicationViewModel>> GetApplicationsAsync(string freelancerId)
    {
        var apps = await _freelancerService.GetApplicationsByFreelancerAsync(freelancerId);
        return apps.Select(a => new ApplicationViewModel
        {
            Id = a.Id,
            FreelancerId = a.FreelancerId,
            Status = a.Status,
            AppliedOn = a.AppliedOn
        }).ToList();
    }

    public async Task<int> ApplyToEventAsync(int eventId, string freelancerId)
    {
        return await _freelancerService.ApplyToEventAsync(eventId, freelancerId);
    }
}
