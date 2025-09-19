using ApplicationCore.Entities.OrganizerAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class OrganizerByIdsSpecification : Specification<Organizer>
{
    public OrganizerByIdsSpecification(IEnumerable<string> organizerIds)
    {
        Query.Where(o => organizerIds.Contains(o.IdentityGuid));
    }
}
