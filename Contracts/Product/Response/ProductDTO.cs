using Grafitist.Misc;
using Grafitist.Misc.Interfaces;

namespace Grafitist.Contracts.Product.Response;

public class ProductDTO : IProduct
{
    public int Id { get; set; }
    public string Code => Utils.Linkify(Item, Color, Variant);
    public string? Description { get; set; }
    public int ItemId { get; set; }
    public ItemDTO? Item { get; set; }
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