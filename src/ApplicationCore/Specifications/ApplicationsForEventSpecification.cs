using ApplicationCore.Entities.EventAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class ApplicationsForEventSpecification : Specification<Application>
{
    public ApplicationsForEventSpecification(int eventId)
    {
        Query.Where(a => a.EventId == eventId);
    }
}
