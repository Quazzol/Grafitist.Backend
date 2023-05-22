using Grafitist.Contracts.Cart.Response;
using Grafitist.Contracts.Payment.Response;
using Grafitist.Contracts.User.Response;
using Grafitist.Misc.Interfaces;
using Misc.Enums;

namespace Grafitist.Contracts.Order.Response;

public class OrderDTO : ITransaction
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserDTO? User { get; set; }
    public Guid CartId { get; set; }
    public CartDTO? Cart { get; set; }
    public string? OrderNumber { get; set; }
    public double Amount { get; set; }
    public OrderStatus Status { get; set; }
    public Guid PaymentId { get; set; }
    public virtual PaymentDTO? Payment { get; set; }
    public DateTime CreatedDate { get; set; }
    public IEnumerable<ITransactionLine>? Lines { get; set; }
}