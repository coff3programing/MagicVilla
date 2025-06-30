using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_API.Models;

public class VillaNumber
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int VillaNo { get; set; }
    [Required]
    public int VillaId { get; set; }

    [ForeignKey("VillaId")]
    public Villa? Villa { get; set; }

    public string? SpecialDetails { get; set; }

    public DateOnly CreatedDate { get; set; }
    public DateOnly UpdateDate { get; set; }
}
