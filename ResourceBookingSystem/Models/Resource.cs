using System.ComponentModel.DataAnnotations;

public class Resource
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public string Location { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be positive.")]
    public int Capacity { get; set; }

    public bool IsAvailable { get; set; }

    // Navigation property is now nullable
    public List<Booking>? Bookings { get; set; } = new();
}
