using Grafitist.Contracts.Order.Response;
using Grafitist.Contracts.User.Response;

namespace Grafitist.Contracts.Payment.Response;

public class PaymentDTO
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public OrderDTO? Order { get; set; }
    public Guid UserId { get; set; }
    public UserDTO? User { get; set; }
    public string? FullName { get; set; }
    public double Amount { get; set; }
    public DateTime CreatedDate { get; set; }
}