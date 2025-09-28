using ApplicationCore.Entities.OrganizerAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class OrganizerByIdentityGuidSpecification : Specification<Organizer>
{
    public OrganizerByIdentityGuidSpecification(string identityGuid)
    {
        Query.Where(o => o.IdentityGuid == identityGuid);
    }
}
