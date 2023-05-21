using System.ComponentModel.DataAnnotations;
using Misc.Enums;

namespace Grafitist.Contracts.Order.Request;

public class OrderInsertDTO
{
    [Required] public Guid UserId { get; set; }
    public Guid CartId { get; set; }
    public double Amount { get; set; }
    public OrderStatus Status { get; set; }
}