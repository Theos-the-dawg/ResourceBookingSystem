using System.ComponentModel.DataAnnotations;
///<Summary>
/// Resource are the entities that can be booked in the Resource Booking System.
///</Summary>
public class Resource
{
    /// <summary>
    ///  resource id for tracking and indexing
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// name of the Resource in question
    /// </summary>
    [Required]
    public string Name { get; set; }
    /// <summary>
    /// short description of the Resource
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// Where the Resource is located
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    /// How many people it can hold or how many items it can accommodate
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be positive.")]
    public int Capacity { get; set; }

    /// <summary>
    /// availability status of the Resource
    /// </summary>
    public bool IsAvailable { get; set; }

    // Navigation property is now nullable
    public List<Booking>? Bookings { get; set; } = new();
}
