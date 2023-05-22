using System.ComponentModel.DataAnnotations;
using Grafitist.Misc.Interfaces;
using Misc.Enums;

namespace Grafitist.Contracts.Order.Request;

public class OrderInsertDTO : ITransaction
{
    public Guid Id { get; } = Guid.NewGuid();
    [Required] public Guid UserId { get; set; }
    public Guid CartId { get; set; }
    public double Amount { get; set; }
    public OrderStatus Status { get; set; }
    public IEnumerable<ITransactionLine>? Lines { get; set; }
}