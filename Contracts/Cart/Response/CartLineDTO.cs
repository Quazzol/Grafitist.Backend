using Grafitist.Contracts.Product.Response;

namespace Grafitist.Contracts.Cart.Response;

public class CartLineDTO
{
    public Guid Id { get; set; }
    public int ProductId { get; set; }
    public ProductDTO? Product { get; set; }
    public int Quantity { get; set; }
    public double Amount { get; set; }
    public Guid CartId { get; set; }
}