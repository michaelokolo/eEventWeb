using ApplicationCore.Entities.EventAggregate;

namespace Web.ViewModels;

public class ApplicationViewModel
{
    public int Id { get; set; }
    public string? FreelancerId { get; set; }
    public ApplicationStatus Status { get; set; }
    public DateTime AppliedOn { get; set; }
}
