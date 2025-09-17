using ApplicationCore.Entities.EventAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class EventWithApplicationsSpecification : Specification<Event>
{
    public EventWithApplicationsSpecification(int eventId)
    {
        Query.Where(e => e.Id == eventId)
             .Include(e => e.Applications);
    }
}
