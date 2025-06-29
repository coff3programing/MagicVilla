using System;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.DTO;

public class VillaCreateDTO
{
    [Required]
    [MaxLength(30)]
    public required string Name { get; set; }

    public string? Details { get; set; }
    public double Tariff { get; set; }
    public int Occupants { get; set; }
    public int SquareMeters { get; set; }
    public string? ImageUrl { get; set; }
    public string? Amenity { get; set; }
}
