using ApplicationCore.Entities.OrganizerAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class OrganizerByIdSpecification : Specification<Organizer>
{
    public OrganizerByIdSpecification(string organizerGuid)
    {
        Query.Where(o => o.IdentityGuid == organizerGuid);
    }
}
