using ApplicationCore.Entities.EventAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class EventsByOrganizerSpecification : Specification<Event>
{
    public EventsByOrganizerSpecification(string organizerId)
    {
        Query.Where(e => e.OrganizerId == organizerId);
    }
}
