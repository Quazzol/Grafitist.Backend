using Grafitist.Contracts.Product.Response;
using Grafitist.Misc.Interfaces;

namespace Grafitist.Contracts.Cart.Response;

public class CartLineDTO : ITransactionLine
{
    public Guid Id { get; set; }
    public int ProductId { get; set; }
    public ProductDTO? Product { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double DiscountedPrice { get; set; } = int.MaxValue;
    public double Amount => Quantity * DiscountedPrice;
    public Guid CampaignId { get; set; }
    public Guid CartId { get; set; }
}