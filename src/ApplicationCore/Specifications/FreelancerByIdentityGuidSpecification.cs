using ApplicationCore.Entities.FreelancerAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class FreelancerByIdentityGuidSpecification : Specification<Freelancer>
{
    public FreelancerByIdentityGuidSpecification(string identityGuid)
    {
        Query.Where(f => f.IdentityGuid == identityGuid);
    }
}
