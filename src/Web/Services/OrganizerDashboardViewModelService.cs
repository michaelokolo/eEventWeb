using ApplicationCore.Entities.EventAggregate;
using ApplicationCore.Interfaces;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services;

public class OrganizerDashboardViewModelService : IOrganizerDashboardViewModelService
{
    private readonly IOrganizerService _organizerService;
    private readonly ILogger<OrganizerDashboardViewModelService> _logger;

    public OrganizerDashboardViewModelService(IOrganizerService organizerService, ILogger<OrganizerDashboardViewModelService> logger)
    {
        _organizerService = organizerService;
        _logger = logger;
    }

    public async Task<List<EventItemViewModel>> GetEventsAsync(string organizerId)
    {
        var events = await _organizerService.GetEventsByOrganizerAsync(organizerId);
        return events.Select(e => new EventItemViewModel
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            Date = e.Date,
            PictureUri = e.PictureUri,
            Role = e.RoleInfo?.Role,
            Location = e.RoleInfo?.Location,
            Budget = e.RoleInfo?.Budget,
            Requirements = e.RoleInfo?.Requirements?.Select(r => new RequirementViewModel { 
                Id = r.Id, 
                Description = r.Description
            }).ToList() ?? new List<RequirementViewModel>()
        }).ToList();
    }

    public async Task<List<ApplicationViewModel>> GetApplicationsAsync(int eventId)
    {
        var apps = await _organizerService.GetApplicationsForEventAsync(eventId);
        return apps.Select(a => new ApplicationViewModel
        {
            Id = a.Id,
            FreeLancerId = a.FreelancerId,
            Status = a.Status,
            AppliedOn = a.AppliedOn
        }).ToList();
    }

    public async Task<ApplicationViewModel?> GetApplicationByIdAsync(int eventId, int applicationId)
    {
        var app = await _organizerService.GetApplicationByIdAsync(eventId, applicationId);
        if (app == null) return null;
        return new ApplicationViewModel
        {
            Id = app.Id,
            FreeLancerId = app.FreelancerId,
            Status = app.Status,
            AppliedOn = app.AppliedOn
        };
    }

    public async Task ReviewApplicationAsync(int eventId, int applicationId, ApplicationStatus status)
    {
        await _organizerService.ReviewApplicationAsync(eventId, applicationId, status);
    }

    public async Task<int> CreateEventAsync(string organizerId, CreateEventViewModel model)
    {
        var roleInfo = new EventRoleInfo(model.Role, model.Location, model.Requirements.Select(r => new Requirement(r)), model.Budget);
        return await _organizerService.CreateEventAsync(organizerId, model.Title, model.Description, model.Date, model.PictureUri, roleInfo);
    }
}
