using System.ComponentModel.DataAnnotations;
using Misc.Enums;

namespace Grafitist.Contracts.Order;

public class OrderUpdateDTO
{
    [Required] public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
}