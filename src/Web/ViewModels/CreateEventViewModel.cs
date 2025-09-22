using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels;


public class CreateEventViewModel
{
    [Required]
    public string Title { get; set; } = "";
    [Required]
    public string Description { get; set; } = "";
    [Required]
    public DateTime Date { get; set; } 
    [Required]
    public string PictureUri { get; set; } = "";
    [Required]
    public string Role { get; set; } = "";
    [Required]
    public string Location { get; set; } = "";
    [Required]
    public decimal Budget { get; set; }
    public List<string> Requirements { get; set; } = new();

    public CreateEventViewModel()
    {
        Date = DateTime.Today;
    }
}
