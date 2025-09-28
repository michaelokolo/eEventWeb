using ApplicationCore.Entities.EventAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class EventByOrganizerAndIdSpecification : Specification<Event>
{
    public EventByOrganizerAndIdSpecification(string organizerId, int eventId)
    {
        Query.Where(e => e.OrganizerId == organizerId && e.Id == eventId);
    }
}
