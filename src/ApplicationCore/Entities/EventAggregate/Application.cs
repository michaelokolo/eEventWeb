using ApplicationCore.Entities.FreelancerAggregate;

namespace ApplicationCore.Entities.EventAggregate;

public class Application : BaseEntity
{
    public int EventId { get; private set; }
    public int FreelancerId { get; private set; }
    public ApplicationStatus Status { get; private set; }
    public DateTime AppliedOn { get; private set; }

    #pragma warning disable CS8618 // Required by Entity Framework
    private Application() { }

    public Application(int eventId, int freelancerId)
    {
        EventId = eventId;
        FreelancerId = freelancerId;
        Status = ApplicationStatus.Pending;
        AppliedOn = DateTime.UtcNow;
    }

    public void SetStatus(ApplicationStatus status)
    {
        Status = status;
    }
}
