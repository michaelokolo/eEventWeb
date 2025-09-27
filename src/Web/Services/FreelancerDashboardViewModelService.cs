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
            Description = e.Description,
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
        var eventIds = apps.Select(a => a.EventId).Distinct().ToList();
        var events = await _freelancerService.GetEventsByIdsAsync(eventIds);
        var eventDict = events.ToDictionary(e => e.Id, e => e);

        return apps.Select(a =>
        {
            var evt = eventDict.TryGetValue(a.EventId, out var e) ? e : null;
            return new ApplicationViewModel
            {
                Id = a.Id,
                FreelancerId = a.FreelancerId,
                EventId = a.EventId,
                Status = a.Status,
                AppliedOn = a.AppliedOn,
                Message = a.Message,
                EventTitle = evt?.Title ?? string.Empty,
                Role = evt?.RoleInfo?.Role ?? string.Empty,
                EventDate = evt?.Date ?? default,
                EventLocation = evt?.RoleInfo?.Location ?? string.Empty,
                EventPictureUri = evt?.PictureUri,
                Requirements = evt?.RoleInfo?.Requirements?.Select(r => new RequirementViewModel
                {
                    Id = r.Id,
                    Description = r.Description
                }).ToList() ?? new List<RequirementViewModel>(),
                OrganizerName = evt?.Organizer?.Name
            };
        }).ToList();
        
    }

    public async Task<int> ApplyToEventAsync(int eventId, string freelancerId, string message)
    {
        return await _freelancerService.ApplyToEventAsync(eventId, freelancerId, message);
    }
}
