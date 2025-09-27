using ApplicationCore.Entities.EventAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class EventsByIdsSpecification : Specification<Event>
{
    public EventsByIdsSpecification(IEnumerable<int> eventIds)
    {
        Query.Where(e => eventIds.Contains(e.Id));
    }
}
