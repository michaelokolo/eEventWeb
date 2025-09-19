using ApplicationCore.Entities.EventAggregate;
using ApplicationCore.Entities.OrganizerAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services;

public class EventViewModelService : IEventViewModelService
{
    private readonly ILogger<EventViewModelService> _logger;
    private readonly IReadRepository<Event> _eventRepository;
    private readonly IReadRepository<Organizer> _organizerRepository;

    public EventViewModelService(
        ILoggerFactory loggerFactory,
        IReadRepository<Event> eventRepository,
        IReadRepository<Organizer> organizerRepository
        )
    {
        _logger = loggerFactory.CreateLogger<EventViewModelService>();
        _eventRepository = eventRepository;
        _organizerRepository = organizerRepository;
    }

    public async Task<EventIndexViewModel> GetEvents()
    {
        _logger.LogInformation("Getting events");

        var allEventsSpecification = new AllEventsSpecification();
        var events = await _eventRepository.ListAsync(allEventsSpecification);

        // Fetch all organizer IDs
        var organizerIds = events.Select(e => e.OrganizerId).Distinct().ToList();

        var organizerByIdsSpecification = new OrganizerByIdsSpecification(organizerIds);

        var organizers = await _organizerRepository.ListAsync(organizerByIdsSpecification);

        // Create a dictionary for quick lookup
        var organizerDict = organizers.ToDictionary(o => o.IdentityGuid, o => o.Name ?? "Unknown Organizer");


        var eventViewModels = events.Select(i => new EventItemViewModel()
        {
            Id = i.Id,
            Title = i.Title,
            Description = i.Description,
            Date = i.Date,
            PictureUri = i.PictureUri,
            Role = i.RoleInfo?.Role,
            Location = i.RoleInfo?.Location,
            Budget = i.RoleInfo?.Budget,
            OrganizerName = organizerDict.TryGetValue(i.OrganizerId, out var name) ? name : "Unknown Organizer"
        }).ToList();

        var eventIndexViewModel = new EventIndexViewModel
        {
            Events = eventViewModels
        };

        return eventIndexViewModel;
    }

    public async Task<OrganizerViewModel> GetOrganizerAsync(string organizerId)
    {
        _logger.LogInformation("Getting organizer name for id: {OrganizerId}", organizerId);
        var spec = new OrganizerByIdSpecification(organizerId);
        var organizer = await _organizerRepository.FirstOrDefaultAsync(spec);
        if (organizer == null) 
        {
            _logger.LogWarning("Organizer not found for id: {OrganizerId}", organizerId);
            return new OrganizerViewModel { Id = "", Name = "Unknown Organizer" };
        } 
        return new OrganizerViewModel { Id = organizer.IdentityGuid, Name = organizer.Name };
    }

    public async Task<EventItemViewModel?> GetEventByIdAsync(int id) 
    {
        _logger.LogInformation("Getting event by id: {Id}", id);

        var eventByIdWithRequirementsSpecification = new EventByIdWithRequirementsSpecification(id);
        var eventEntity = await _eventRepository.FirstOrDefaultAsync(eventByIdWithRequirementsSpecification);
        if (eventEntity == null) return null;

        var eventItemViewModel = new EventItemViewModel()
        {
            Id = eventEntity.Id,
            Title = eventEntity.Title,
            Description = eventEntity.Description,
            Date = eventEntity.Date,
            PictureUri = eventEntity.PictureUri,
            Role = eventEntity.RoleInfo?.Role,
            Location = eventEntity.RoleInfo?.Location,
            Budget = eventEntity.RoleInfo?.Budget,
            OrganizerName = (await GetOrganizerAsync(eventEntity.OrganizerId)).Name,
            Requirements = eventEntity.RoleInfo?.Requirements?.Select(r => new RequirementViewModel
            {
                Id = r.Id,
                Description = r.Description
            }).ToList() ?? new List<RequirementViewModel>()
        };

        return eventItemViewModel;
    }

}