using System.ComponentModel.DataAnnotations;

namespace Grafitist.Contracts.Product.Request;

public class VariantInsertDTO
{
    [Required] public string? Code { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}