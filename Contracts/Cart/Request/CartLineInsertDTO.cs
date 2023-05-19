using System.ComponentModel.DataAnnotations;

namespace Grafitist.Contracts.Cart.Request;

public class CartLineInsertDTO
{
    [Required] public int ProductId { get; set; }
    public int Quantity { get; set; }
    public Guid? CartId { get; set; }
}