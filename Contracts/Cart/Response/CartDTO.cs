using Grafitist.Misc.Enums;
using Grafitist.Misc.Interfaces;

namespace Grafitist.Contracts.Cart.Response;

public class CartDTO : ITransaction
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public CartStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public double Amount { get; set; }
    public IEnumerable<ITransactionLine>? Lines { get; set; }
}