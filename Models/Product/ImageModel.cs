using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grafitist.Models.Product;

[Table("Image")]
public class ImageModel
{
    [Key] public int Id { get; set; }
    public bool IsCover { get; set; }
    public bool IsActive { get; set; }
    public string? Type { get; set; }
    public byte[]? Data { get; set; }
    [ForeignKey("Product")]
    public int ProductModelId { get; set; }
}