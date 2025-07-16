﻿using System.ComponentModel.DataAnnotations;

public class Booking
{
    public int Id { get; set; }

    [Required]
    public int ResourceId { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    public string BookedBy { get; set; }

    [Required]
    public string Purpose { get; set; }

    public Resource? Resource { get; set; }
}
