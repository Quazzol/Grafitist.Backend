using System.ComponentModel.DataAnnotations;

namespace Grafitist.Contracts.Cart.Request;

public class CartLineUpdateDTO
{
    [Required] public Guid Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}