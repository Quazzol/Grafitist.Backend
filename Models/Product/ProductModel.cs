using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grafitist.Models.Product;

[Table("Product")]
public class ProductModel
{
    [Key] public int Id { get; set; }
    public string? Description { get; set; }
    public int ItemId { get; set; }
    public ItemModel? Item { get; set; }
    public int? ColorId { get; set; }
    public virtual ColorModel? Color { get; set; }
    public int? VariantId { get; set; }
    public virtual VariantModel? Variant { get; set; }
    public double Price { get; set; }
    public bool IsActive { get; set; }
    public IEnumerable<ImageModel>? Images { get; set; }
}