namespace Web.ViewModels;

public class EventItemViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public string? PictureUri { get; set; }
    public string? Role { get; set; }
    public string? Location { get; set; }
    public decimal? Budget { get; set; }
    public string? OrganizerName { get; set; }
    public List<RequirementViewModel> Requirements { get; set; } = new List<RequirementViewModel>();
}
