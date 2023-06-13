using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Grafitist.Models.Stock;

namespace Grafitist.Models.Product;

[Table("Product")]
public class ProductModel
{
    [Key] public int Id { get; set; }
    [Required] public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    [Required] public int CategoryId { get; set; }
    public CategoryModel? Category { get; set; }
    [Required] public int MaterialId { get; set; }
    public MaterialModel? Material { get; set; }
    public int? ColorId { get; set; }
    public virtual ColorModel? Color { get; set; }
    public int? VariantId { get; set; }
    public virtual VariantModel? Variant { get; set; }
    public double Price { get; set; }
    public bool IsActive { get; set; }
    public StockModel? Stock { get; set; }
    public IEnumerable<ImageModel>? Images { get; set; }
}