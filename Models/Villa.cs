
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_API.Models;

[Table("Villa")]
public class Villa
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Details { get; set; }
    [Required]
    public double Tariff { get; set; }
    public int Occupants { get; set; }
    public int SquareMeters { get; set; }
    public string? ImageUrl { get; set; }
    public string? Amenity { get; set; }
    public DateOnly CreatedDate { get; set; }
    public DateOnly UpdateDate { get; set; }
}
