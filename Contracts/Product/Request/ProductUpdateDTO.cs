using System.ComponentModel.DataAnnotations;

namespace Grafitist.Contracts.Product.Request;

public class ProductUpdateDTO
{
    public string? Description { get; set; }
    [Required] public int Id { get; set; }
    public int ItemId { get; set; }
    public int? ColorId { get; set; }
    public int? VariantId { get; set; }
    public double Price { get; set; }
    public bool IsActive { get; set; }
}