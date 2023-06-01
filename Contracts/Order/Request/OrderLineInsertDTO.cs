using System.ComponentModel.DataAnnotations;
using Grafitist.Misc.Interfaces;

namespace Grafitist.Contracts.Order.Request;

public class OrderLineInsertDTO : ITransactionLine
{
    [Required] public int ProductId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double DiscountedPrice { get; set; } = int.MaxValue;
    public double Amount => Quantity * DiscountedPrice;
    public Guid CampaignId { get; set; }
    [Required] public Guid OrderId { get; set; }
}