using System.ComponentModel.DataAnnotations;

namespace Grafitist.Contracts.Product.Request;

public class ProductInsertDTO
{
    [Required] public string? Code { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    [Required] public int CategoryId { get; set; }
    [Required] public int MaterialId { get; set; }
    public int? ColorId { get; set; }
    public int? VariantId { get; set; }
    public double Price { get; set; }
    public bool IsActive { get; set; } = true;
}