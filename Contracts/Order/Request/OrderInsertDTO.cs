using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Misc.Enums;

namespace Grafitist.Contracts.Order;

[Table("Order")]
public class OrderInsertDTO
{
    [Required] public Guid UserId { get; set; }
    public Guid CartId { get; set; }
    public OrderStatus Status { get; set; }
}