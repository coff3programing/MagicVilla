using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.DTO;

public class NumberVillaUpdateDto
{
    [Required]
    public int VillaNo { get; set; }
    [Required]
    public int VillaId { get; set; }
    public string? SpecialDetails { get; set; }
}
