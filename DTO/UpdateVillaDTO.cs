using System;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.DTO;

public class VillaUpdateDTO
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public required string Name { get; set; }

    public string? Details { get; set; }

    [Required]
    public double Tariff { get; set; }

    [Required]
    public int Occupants { get; set; }

    [Required]
    public int SquareMeters { get; set; }

    [Required]
    public string? ImageUrl { get; set; }

    public string? Amenity { get; set; }
}
