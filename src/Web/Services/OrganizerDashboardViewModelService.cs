using ApplicationCore.Entities.EventAggregate;
using ApplicationCore.Interfaces;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services;

public class OrganizerDashboardViewModelService : IOrganizerDashboardViewModelService
{
    private readonly IOrganizerService _organizerService;
    private readonly IUserProfileService _userProfileService;
    private readonly ILogger<OrganizerDashboardViewModelService> _logger;

    public OrganizerDashboardViewModelService(IOrganizerService organizerService, IUserProfileService userProfileService, ILogger<OrganizerDashboardViewModelService> logger)
    {
        _organizerService = organizerService;
        _userProfileService = userProfileService;
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

    public async Task<List<ApplicationViewModel>> GetApplicationsAsync(int eventId, string organizerId)
    {
        var apps = await _organizerService.GetApplicationsForEventAsync(eventId);
        var evt = await _organizerService.GetEventByOrganizerAndIdAsync(organizerId, eventId);

        var viewModelTasks = apps.Select(async a =>
        {
            var freelancerName = await _userProfileService.GetFreelancerNameAsync(a.FreelancerId);

            return new ApplicationViewModel
            {
                Id = a.Id,
                EventTitle = evt?.Title ?? "",
                FreelancerId = a.FreelancerId,
                Status = a.Status,
                AppliedOn = a.AppliedOn,
                FreelancerName = freelancerName,
            };
        });

        var viewModels = await Task.WhenAll(viewModelTasks);
        return viewModels.ToList();
    }

    public async Task<ApplicationViewModel?> GetApplicationByIdAsync(int eventId, int applicationId, string organizerId)
    {
        var app = await _organizerService.GetApplicationByIdAsync(eventId, applicationId);
        var freelancerName = await _userProfileService.GetFreelancerNameAsync(app?.FreelancerId ?? "");
        var evt = await _organizerService.GetEventByOrganizerAndIdAsync(organizerId, eventId);
        if (app == null) return null;
        return new ApplicationViewModel
        {
            Id = app.Id,
            FreelancerId = app.FreelancerId,
            FreelancerName = freelancerName,
            EventTitle = evt?.Title ?? "",
            Role = evt?.RoleInfo?.Role ?? "",
            EventDate = evt?.Date ?? DateTime.MinValue,
            
            Status = app.Status,
            AppliedOn = app.AppliedOn,
            Message = app.Message,
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

    public async Task<EventItemViewModel> GetEventByIdAsync(int eventId, string organizerId)
    {
        var evt = await _organizerService.GetEventByOrganizerAndIdAsync(organizerId, eventId);
        if (evt == null)
        {
            _logger.LogWarning("Event with ID {EventId} not found for Organizer {OrganizerId}", eventId, organizerId);
            throw new KeyNotFoundException($"Event with ID {eventId} not found.");
        }
        return new EventItemViewModel
        {
            Id = evt.Id,
            Title = evt.Title,
            Description = evt.Description,
            Date = evt.Date,
            PictureUri = evt.PictureUri,
            Role = evt.RoleInfo?.Role,
            Location = evt.RoleInfo?.Location,
            Budget = evt.RoleInfo?.Budget,
            Requirements = evt.RoleInfo?.Requirements?.Select(r => new RequirementViewModel
            {
                Id = r.Id,
                Description = r.Description
            }).ToList() ?? new List<RequirementViewModel>()
        };

    }
}
