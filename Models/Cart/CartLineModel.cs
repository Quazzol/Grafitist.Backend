using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Grafitist.Models.Product;

namespace Grafitist.Models.Cart;

[Table("CartLine")]
public class CartLineModel
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    [Required] public int ProductId { get; set; }
    public virtual ProductModel? Product { get; set; }
    public int Quantity { get; set; }
    public double Amount { get; set; }
    [Required] public Guid CartId { get; set; }
}