using ApplicationCore.Entities.EventAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class AvailableEventsForFreelancerSpecification : Specification<Event>
{
    public AvailableEventsForFreelancerSpecification(string freelancerId)
    {
        Query.Where(e => !e.Applications.Any(a => a.FreelancerId == freelancerId));
    }
}
