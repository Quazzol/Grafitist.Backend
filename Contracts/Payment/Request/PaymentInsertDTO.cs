using System.ComponentModel.DataAnnotations;

namespace Grafitist.Contracts.Payment.Request;

public class PaymentInsertDTO
{
    [Required] public Guid UserId { get; set; }
    [Required] public Guid OrderId { get; set; }
    public string? FullName { get; set; }
    public double Amount { get; set; }
    public string? Hash { get; set; }
}