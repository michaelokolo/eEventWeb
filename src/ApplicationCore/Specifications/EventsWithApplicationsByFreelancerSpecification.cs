using ApplicationCore.Entities.EventAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class EventsWithApplicationsByFreelancerSpecification : Specification<Event>
{
    public EventsWithApplicationsByFreelancerSpecification(string freelancerId)
    {
        Query.Where(e => e.Applications.Any(a => a.FreelancerId == freelancerId));
        Query.Include(e => e.Applications);
    }
}
