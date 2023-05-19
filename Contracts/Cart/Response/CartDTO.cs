using Grafitist.Misc.Enums;

namespace Grafitist.Contracts.Cart.Response;

public class CartDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public double Amount { get; set; }
    public CartStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public ICollection<CartLineDTO>? Lines { get; set; }
}