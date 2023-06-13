using System.ComponentModel.DataAnnotations;

namespace Grafitist.Contracts.Product.Request;

public class ProductUpdateDTO
{
    [Required] public int Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public int CategoryId { get; set; }
    public int MaterialId { get; set; }
    public int? ColorId { get; set; }
    public int? VariantId { get; set; }
    public double Price { get; set; }
    public bool IsActive { get; set; }
}