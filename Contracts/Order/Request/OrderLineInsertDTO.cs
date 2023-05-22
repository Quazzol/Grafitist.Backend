using System.ComponentModel.DataAnnotations;
using Grafitist.Misc.Interfaces;

namespace Grafitist.Contracts.Order.Request;

public class OrderLineInsertDTO : ITransactionLine
{
    [Required] public int ProductId { get; set; }
    public double Amount { get; set; }
    public int Quantity { get; set; }
    public Guid CampaignId { get; set; }
    [Required] public Guid OrderId { get; set; }
}