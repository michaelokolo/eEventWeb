using ApplicationCore.Entities.OrganizerAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class OrganizerByIdsSpecification : Specification<Organizer>
{
    public OrganizerByIdsSpecification(IEnumerable<int> organizerIds)
    {
        Query.Where(o => organizerIds.Contains(o.Id));
    }
}
