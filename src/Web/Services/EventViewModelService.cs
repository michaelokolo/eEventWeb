using ApplicationCore.Entities.EventAggregate;
using ApplicationCore.Interfaces;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services;

public class EventViewModelService : IEventViewModelService
{
    private readonly ILogger<EventViewModelService> _logger;
    private readonly IRepository<Event> _eventRepository;

    public EventViewModelService(
        ILoggerFactory loggerFactory,
        IRepository<Event> eventRepository)
    {
        _logger = loggerFactory.CreateLogger<EventViewModelService>();
        _eventRepository = eventRepository;
    }

    public async Task<EventIndexViewModel> GetEvents()
    {
        _logger.LogInformation("Getting events");
        var events = await _eventRepository.ListAsync();

        var eventIndexViewModel = new EventIndexViewModel()
        {
            Events = events.Select(i => new EventItemViewModel()
            {
                Id = i.Id,
                Title = i.Title,
                Description = i.Description,
                Date = i.Date,
                PictureUri = i.PictureUri
            }).ToList()
        };

        return eventIndexViewModel;
    }

    public async Task<EventItemViewModel?> GetEventByIdAsync(int id) 
    {
        _logger.LogInformation("Getting event by id: {Id}", id);
        var eventEntity = await _eventRepository.GetByIdAsync(id);
        if (eventEntity == null) return null;

        var eventItemViewModel = new EventItemViewModel()
        {
            Id = eventEntity.Id,
            Title = eventEntity.Title,
            Description = eventEntity.Description,
            Date = eventEntity.Date,
            PictureUri = eventEntity.PictureUri
        };

        return eventItemViewModel;
    }

}