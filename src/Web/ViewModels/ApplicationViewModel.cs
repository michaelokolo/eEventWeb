using ApplicationCore.Entities.EventAggregate;

namespace Web.ViewModels;

public class ApplicationViewModel
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string? FreelancerId { get; set; }
    public string? FreelancerName { get; set; }
    public string EventTitle { get; set; } = "";
    public string Role { get; set; } = "";
    public DateTime EventDate { get; set; }
    public string EventLocation { get; set; } = "";
    public string? EventPictureUri { get; set; }
    public List<RequirementViewModel> Requirements { get; set; } = new();
    public string? OrganizerName { get; set; }
    public ApplicationStatus Status { get; set; }
    public DateTime AppliedOn { get; set; }
    public string? Message { get; set; }
    
}
