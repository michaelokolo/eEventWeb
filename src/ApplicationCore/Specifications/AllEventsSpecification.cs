using Ardalis.Specification;
using ApplicationCore.Entities.EventAggregate;

namespace ApplicationCore.Specifications;

public class AllEventsSpecification : Specification<Event>
{
    public AllEventsSpecification()
    {
        Query.OrderBy(e => e.Date);
    }
}
