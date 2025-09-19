using ApplicationCore.Entities.EventAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class EventFilterPaginatedSpecification : Specification<Event>
{
    public EventFilterPaginatedSpecification(
        int skip, int take, 
        string? organizerId = null, 
        DateTime? fromDate = null,
        DateTime? toDate = null,
        string? keyword = null)
    {
        if (take == 0)
            take = int.MaxValue;

        Query
            .Where(e =>
            (!string.IsNullOrEmpty(organizerId) || e.OrganizerId == organizerId) &&
            (!fromDate.HasValue || e.Date >= fromDate) &&
            (!toDate.HasValue || e.Date <= toDate) &&
            (string.IsNullOrEmpty(keyword) ||
                e.Title.Contains(keyword) ||
                e.Description.Contains(keyword)))
            .Skip(skip).Take(take);
    }
}
