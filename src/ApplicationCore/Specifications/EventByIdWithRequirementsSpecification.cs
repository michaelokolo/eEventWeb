using ApplicationCore.Entities.EventAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class EventByIdWithRequirementsSpecification : Specification<Event>
{
    public EventByIdWithRequirementsSpecification(int eventId)
    {
        Query.Where(e => e.Id == eventId)
            .Include(e => e.RoleInfo)
            .ThenInclude(ri => ri!.Requirements);
    }
}
