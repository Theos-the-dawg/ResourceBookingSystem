using System.ComponentModel.DataAnnotations;
///<Summary>
///  The Booking class model is linked with the Resource class model. it represents a booking made for a resource in the Resource Booking System.
///</Summary>
public class Booking
{

    /// <summary>
    /// Booking ID for tracking 
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// resource ID for linking to the Resource being booked
    /// </summary>
    [Required]
    public int ResourceId { get; set; }
    /// <summary>
    /// start and end time of the booking
    /// </summary>

    [Required]
    public DateTime StartTime { get; set; }
    /// <summary>
    /// end time of the booked resource for availing
    /// </summary>
    [Required]
    public DateTime EndTime { get; set; }
    /// <summary>
    /// which user booked the system
    /// </summary>

    [Required]
    public string BookedBy { get; set; }
    /// <summary>
    /// reason for booking the resource
    /// </summary>
    [Required]
    public string Purpose { get; set; }
    ///
    public Resource? Resource { get; set; }
}
