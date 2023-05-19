using Grafitist.Contracts.Cart.Response;
using Grafitist.Contracts.User.Response;
using Misc.Enums;

namespace Grafitist.Contracts.Order;

public class OrderDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserDTO? User { get; set; }
    public Guid CartId { get; set; }
    public CartDTO? Cart { get; set; }
    public string? OrderNumber { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
}