using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Grafitist.Models.Cart;
using Grafitist.Models.User;
using Misc.Enums;

namespace Grafitist.Models.Order;

[Table("Order")]
public class OrderModel
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    [Required] public Guid UserId { get; set; }
    public virtual UserModel? User { get; set; }
    public Guid CartId { get; set; }
    public virtual CartModel? Cart { get; set; }
    public string? OrderNumber { get; set; }
    public double Amount { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public IEnumerable<OrderLineModel>? Lines { get; set; }
}