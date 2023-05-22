namespace Grafitist.Contracts.Product.Response;

public class ProductDTO
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public int ItemId { get; set; }
    public ItemDTO? Item { get; set; }
    public int? ColorId { get; set; }
    public ColorDTO? Color { get; set; }
    public int? VariantId { get; set; }
    public VariantDTO? Variant { get; set; }
    public double Price { get; set; }
    public double CampaignPrice { get; set; }
    public bool IsActive { get; set; }
    public IEnumerable<ImageDTO>? Images { get; set; }
}