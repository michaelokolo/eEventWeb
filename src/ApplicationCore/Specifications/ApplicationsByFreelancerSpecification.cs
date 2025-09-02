using ApplicationCore.Entities.EventAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class ApplicationsByFreelancerSpecification : Specification<Application>
{
    public ApplicationsByFreelancerSpecification(int freelancerId)
    {
        Query.Where(a => a.FreelancerId == freelancerId);
    }
}
