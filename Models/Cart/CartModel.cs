using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Grafitist.Misc.Enums;

namespace Grafitist.Models.Cart;

[Table("Cart")]
public class CartModel
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    [Required] public Guid UserId { get; set; }
    public double Amount { get; set; }
    public CartStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public ICollection<CartLineModel>? Lines { get; set; }
}