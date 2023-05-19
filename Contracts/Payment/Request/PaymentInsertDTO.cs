using System.ComponentModel.DataAnnotations;

namespace Grafitist.Contracts.Payment;

public class PaymentInsertDTO
{
    [Required] public Guid UserId { get; set; }
    [Required] public Guid OrderId { get; set; }
    public string? FullName { get; set; }
    public double Amount { get; set; }
}