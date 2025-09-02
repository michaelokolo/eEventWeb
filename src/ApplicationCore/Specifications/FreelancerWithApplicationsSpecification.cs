using ApplicationCore.Entities.FreelancerAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class FreelancerWithApplicationsSpecification : Specification<Freelancer>
{
    public FreelancerWithApplicationsSpecification(int freelancerId)
    {
        Query
        .Where(f => f.Id == freelancerId)
        .Include(f => f.Applications);
    }
}
