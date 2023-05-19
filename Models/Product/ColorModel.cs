using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grafitist.Models.Product;

[Table("Color")]
public class ColorModel
{
    [Key] public int Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? HexCode { get; set; }
    public bool IsActive { get; set; }
}