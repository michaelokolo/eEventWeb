using ApplicationCore.Entities.EventAggregate;
using Ardalis.Specification;

namespace ApplicationCore.Specifications;

public class ApplicationsByStatusSpecification : Specification<Application>
{

    public ApplicationsByStatusSpecification(ApplicationStatus status)
    {

    }
}
