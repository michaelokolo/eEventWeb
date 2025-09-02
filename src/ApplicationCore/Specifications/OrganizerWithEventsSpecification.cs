using ApplicationCore.Entities.OrganizerAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class OrganizerWithEventsSpecification : Specification<Organizer>
{
    public OrganizerWithEventsSpecification(int organizerId)
    {
        Query
            .Where(o => o.Id == organizerId)
            .Include(o => o.Events);
    }
}
