namespace Web.ViewModels;

public class EventItemViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public string? PictureUri { get; set; }
}
