using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grafitist.Models.Product;

[Table("Variant")]
public class VariantModel
{
    [Key] public int Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}