using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Grafitist.Models.Campaign;
using Grafitist.Models.Product;

namespace Grafitist.Models.Order;

[Table("OrderLine")]
public class OrderLineModel
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    [Required] public int ProductId { get; set; }
    public virtual ProductModel? Product { get; set; }
    public double Amount { get; set; }
    public int Quantity { get; set; }
    public Guid CampaignId { get; set; }
    public virtual CampaignModel? Campaign { get; set; }
    [Required] public Guid OrderId { get; set; }
}