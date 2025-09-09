using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;

namespace ApplicationCore.Entities.EventAggregate;

public class Event : BaseEntity, IAggregateRoot
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime Date { get; private set; }
    public string PictureUri { get; private set; }
    public int OrganizerId { get; private set; }

    private readonly List<Application> _applications = new();
    public IReadOnlyCollection<Application> Applications => _applications.AsReadOnly();


    #pragma warning disable CS8618 // Required by Entity Framework
    private Event() { }

    public Event(string title, 
        string description, 
        DateTime date,
        string pictureUri,
        int organizerId)
    {
        Guard.Against.NullOrEmpty(title, nameof(title));
        Guard.Against.NullOrEmpty(description, nameof(description));
        Guard.Against.OutOfRange(date, nameof(date), DateTime.UtcNow, DateTime.MaxValue);
        Guard.Against.NullOrEmpty(pictureUri, nameof(pictureUri));
        Guard.Against.NegativeOrZero(organizerId, nameof(organizerId));

        Title = title;
        Description = description;
        Date = date;
        PictureUri = pictureUri;
        OrganizerId = organizerId;
    }

    public Application Apply(int freelancerId)
    {
        Guard.Against.NegativeOrZero(freelancerId, nameof(freelancerId));

        if (_applications.Exists(a => a.FreelancerId == freelancerId))
        {
            throw new InvalidOperationException("Freelancer has already applied to this event.");
        }

        var application = new Application(this.Id, freelancerId);
        _applications.Add(application);
        return application;
    }

    public void ReviewApplication(int applicationId, ApplicationStatus status)
    {
        var application = _applications.Find(a => a.Id == applicationId);
        if (application == null)
        {
            throw new InvalidOperationException("Application not found.");
        }
        application.SetStatus(status);
    }

}
