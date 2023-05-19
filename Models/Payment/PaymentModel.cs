using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Grafitist.Models.Order;
using Grafitist.Models.User;

namespace Grafitist.Models.Payment;

[Table("Payment")]
public class PaymentModel
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    [Required] public Guid OrderId { get; set; }
    public virtual OrderModel? Order { get; set; }
    [Required] public Guid UserId { get; set; }
    public virtual UserModel? User { get; set; }
    public string? FullName { get; set; }
    public double Amount { get; set; }
    public DateTime CreatedDate { get; set; }
}