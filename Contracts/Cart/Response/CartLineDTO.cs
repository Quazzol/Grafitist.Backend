using Grafitist.Contracts.Product.Response;
using Grafitist.Misc.Interfaces;

namespace Grafitist.Contracts.Cart.Response;

public class CartLineDTO : IHasCalculatableAmount
{
    public Guid Id { get; set; }
    public int ProductId { get; set; }
    public ProductDTO? Product { get; set; }
    public int Quantity { get; set; }
    public double Amount { get; set; }
    public Guid CampaignId { get; set; }
    public Guid CartId { get; set; }
}