using Grafitist.Contracts.Campaign.Response;
using Grafitist.Contracts.Product.Response;
using Grafitist.Misc.Interfaces;

namespace Grafitist.Contracts.Order.Response;

public class OrderLineDTO : ITransactionLine
{
    public Guid Id { get; set; }
    public int ProductId { get; set; }
    public virtual ProductDTO? Product { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double DiscountedPrice { get; set; } = int.MaxValue;
    public double Amount => Quantity * DiscountedPrice;
    public Guid CampaignId { get; set; }
    public virtual CampaignDTO? Campaign { get; set; }
    public Guid OrderId { get; set; }
}