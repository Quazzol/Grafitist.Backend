using Grafitist.Misc;
using Grafitist.Misc.Interfaces;

namespace Grafitist.Contracts.Product.Response;

public class ProductDTO : IProduct
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public string Link => Utils.Linkify(Description, Color, Variant);
    public int CategoryId { get; set; }
    public CategoryDTO? Category { get; set; }
    public int MaterialId { get; set; }
    public MaterialDTO? Material { get; set; }
    public int? ColorId { get; set; }
    public ColorDTO? Color { get; set; }
    public int? VariantId { get; set; }
    public VariantDTO? Variant { get; set; }
    public double Price { get; set; }
    public double DiscountedPrice { get; set; } = int.MaxValue;
    public bool IsActive { get; set; }
    public IEnumerable<ImageDTO>? Images { get; set; }
    int IProduct.ProductId => Id;
}